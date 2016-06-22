using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;
using Newtonsoft.Json;

namespace WorkEstimateRequester
{
    public partial class RequesterForm : Form
    {
        /// <summary>
        /// The exachange name to use
        /// </summary>
        const string DEFAULT_EXCHANGE = "default";

        /// <summary>
        /// The queue to push messages to
        /// </summary>
        const string SEND_QUEUE = "general_queue";

        /// <summary>
        /// The messagetype we are sending
        /// </summary>
        const string SEND_TYPE = "WORKREQUEST";

        /// <summary>
        /// The host we are running RabbitMQ on
        /// </summary>
        const string HOST = "xtyx.nl";

        /// <summary>
        /// The routing key to use, irrelevant for this appliaction, so blank
        /// </summary>
        const string ROUTING_KEY_SEND = "";

        /// <summary>
        /// The messagenumber we are currently at
        /// </summary>
        private int messageNumber = 0;

        /// <summary>
        /// The list of messages we have sent, keyed by id so we can identify responses
        /// </summary>
        private Dictionary<int, RequestReplyPair> messages = new Dictionary<int, RequestReplyPair>();


        /// <summary>
        /// The name of the queue we recieve messages on
        /// </summary>
        string RECV_QUEUE;

        /// <summary>
        /// The connection to RabbitMQ
        /// </summary>
        IConnection connection;

        /// <summary>
        /// The channel we are sending on
        /// </summary>
        IModel send_channel;

        /// <summary>
        /// The channel we are recieving on
        /// </summary>
        IModel recv_channel;


        public RequesterForm()
        {
            // Setup form
            InitializeComponent();

            // Create connection
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = HOST;
            connection = factory.CreateConnection();

            // Create send channel and queue
            send_channel = connection.CreateModel();
            send_channel.ExchangeDeclare(DEFAULT_EXCHANGE, ExchangeType.Direct);
            send_channel.QueueDeclare(SEND_QUEUE, false, false, false, null);

            // Create recieve queue
            // Generate a random guid for the current application
            // This is used as delivery address and recieve queue
            RECV_QUEUE = System.Guid.NewGuid().ToString();
            send_channel.QueueBind(SEND_QUEUE, DEFAULT_EXCHANGE, ROUTING_KEY_SEND);

            recv_channel = connection.CreateModel();
            recv_channel.ExchangeDeclare(DEFAULT_EXCHANGE, ExchangeType.Direct);
            recv_channel.QueueDeclare(RECV_QUEUE, false, false, false, null);
            recv_channel.QueueBind(RECV_QUEUE, DEFAULT_EXCHANGE, RECV_QUEUE);

            // Setup listener for messages
            setupEventListener();
        }

        private void setupEventListener()
        {
            // Setup a consumer that will raise an event when a message arrives
            RabbitMQ.Client.Events.EventingBasicConsumer consumer = new RabbitMQ.Client.Events.EventingBasicConsumer(recv_channel);
            consumer.Received += (ch, msg) =>
            {
                string body = new string(Encoding.UTF8.GetChars(msg.Body));

                // Parse json to response
                WorkResponse resp = JsonConvert.DeserializeObject<WorkResponse>(body);

                // Match response to sent message
                if (messages.ContainsKey(resp.Id))
                {
                    messages[resp.Id].Workresponse = resp;
                } else
                {
                    Console.WriteLine("Invalid message id, ignoring");
                }
                this.updateListBoxes();          
            };
            String consumertag = recv_channel.BasicConsume(RECV_QUEUE, true, consumer);
        }

        /// <summary>
        /// Updates the listboxes to differentiate between open and closed requests
        /// </summary>
        private void updateListBoxes()
        {
            this.Invoke((MethodInvoker)(() => {
                lstClosed.Items.Clear();
                lstOpen.Items.Clear();
                foreach (RequestReplyPair pair in messages.Values)
                {
                    if (pair.hasResponse())
                    {
                        lstClosed.Items.Add(pair);
                    }
                    else
                    {
                        lstOpen.Items.Add(pair);
                    }
                }
            }));
        }

        /// <summary>
        /// Displays a requestresponsepair
        /// </summary>
        /// <param name="pair">The pair to display</param>
        private void displayPair(RequestReplyPair pair)
        {
            if (pair.Workrequest != null)
            {
                txtWorktype.Text = pair.Workrequest.Worktype;
                txtDescription.Text = pair.Workrequest.Description;
            }
            if (pair.Workresponse != null)
            {
                lblEstimatedDone.Text = pair.Workresponse.estimatedDone.ToShortDateString();
                lblNotes.Text = pair.Workresponse.Notes;
            }
        }

        /// <summary>
        /// Handles the sending of a message
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            // Increase message number to get a unique id
            messageNumber++;

            // Create a workrequest object with the entered data and an id
            WorkRequest req = new WorkRequest(messageNumber, txtWorktype.Text, txtDescription.Text);

            // Create a RequestReplyPair to store the request and later response in
            RequestReplyPair pair = new RequestReplyPair();
            pair.Workrequest = req;
            messages.Add(req.Id, pair);

            // Encode the message to json
            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(req));
            IBasicProperties props = send_channel.CreateBasicProperties();

            // Add message headers
            Dictionary<string, object> headers = new Dictionary<string, object>();
            props.Headers = headers;
            props.Headers.Add("reply_to", RECV_QUEUE);
            props.Headers.Add("message_type", SEND_TYPE);
            props.ContentType = "text/plain";
            props.DeliveryMode = 2;

            // Send the message
            send_channel.BasicPublish(DEFAULT_EXCHANGE, ROUTING_KEY_SEND, props, messageBodyBytes);
            updateListBoxes();
        }
        
        /// <summary>
        /// Handles the selection of an item in the listbox of open requests
        /// </summary>
        private void lstOpen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOpen.SelectedItem == null)
            {
                return;
            }
            displayPair((RequestReplyPair) lstOpen.SelectedItem);
        }

        /// <summary>
        /// Handles the selection of an item in the listbox of open requests
        /// </summary>
        private void lstClosed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstClosed.SelectedItem == null)
            {
                return;
            }
            displayPair((RequestReplyPair)lstClosed.SelectedItem);
        }

        private void RequesterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the connection
            send_channel.Close(200, "Quit");
            recv_channel.Close(200, "Quit");
            connection.Close();
        }
    }
}
