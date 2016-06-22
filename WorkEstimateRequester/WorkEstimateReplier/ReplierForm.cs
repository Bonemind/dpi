using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace WorkEstimateReplier
{
    public partial class ReplierForm : Form
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
        const string SEND_TYPE = "WORKRESPONSE";

        /// <summary>
        /// The host we are running RabbitMQ on
        /// </summary>
        const string HOST = "xtyx.nl";

        /// <summary>
        /// The name of the queue we recieve messages on
        /// </summary>
        const string RECV_QUEUE = "request_processor_queue";

        /// <summary>
        /// The routing key to use, irrelevant for this appliaction, so blank
        /// </summary>
        string ROUTING_KEY_SEND = "";

        /// <summary>
        /// The list of messges we are handling or have handled
        /// </summary>
        private List<RequestReplyPair> messages = new List<RequestReplyPair>();

        /// <summary>
        /// The RequestReplyPair we are working on
        /// </summary>
        private RequestReplyPair currentPair = null;


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

        public ReplierForm()
        {
            // Setup form
            InitializeComponent();

            // Setup connection
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = HOST;
            connection = factory.CreateConnection();

            // Setup send channel
            send_channel = connection.CreateModel();
            send_channel.ExchangeDeclare(DEFAULT_EXCHANGE, ExchangeType.Direct);
            send_channel.QueueDeclare(SEND_QUEUE, false, false, false, null);
            send_channel.QueueBind(SEND_QUEUE, DEFAULT_EXCHANGE, ROUTING_KEY_SEND);

            // Setup recieve cahnnel
            recv_channel = connection.CreateModel();
            recv_channel.ExchangeDeclare(DEFAULT_EXCHANGE, ExchangeType.Direct);
            recv_channel.QueueDeclare(RECV_QUEUE, false, false, false, null);
            recv_channel.QueueBind(RECV_QUEUE, DEFAULT_EXCHANGE, RECV_QUEUE);

            // Empty list of requests
            lstOpenRequests.Items.Clear();

            // Set up message listener
            setupEventListener();
        }

        private void setupEventListener()
        {
            // Setup a consumer that will raise an event when a message arrives
            RabbitMQ.Client.Events.EventingBasicConsumer consumer = new RabbitMQ.Client.Events.EventingBasicConsumer(recv_channel);
            consumer.Received += (ch, msg) =>
            {
                // Parse json to request
                string body = new string(Encoding.UTF8.GetChars(msg.Body));
                WorkRequest req = JsonConvert.DeserializeObject<WorkRequest>(body);

                // Get the address we should reply to
                object replyTo;
                msg.BasicProperties.Headers.TryGetValue("reply_to", out replyTo);
                req.ReturnAddress = new string(Encoding.UTF8.GetChars((Byte[])replyTo));

                // Create a requestreplypair
                RequestReplyPair pair = new RequestReplyPair();
                pair.Workrequest = req;
                
                // Add the pair to the list of things we have to handle
                messages.Add(pair);
                this.Invoke((MethodInvoker)(() => lstOpenRequests.Items.Add(pair)));
            };
            String consumertag = recv_channel.BasicConsume(RECV_QUEUE, true, consumer);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles selecting of a message
        /// </summary>
        private void lstOpenRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If we haven't selected anything, there's nothing to do
            if (lstOpenRequests.SelectedItem == null)
            {
                return;
            }

            // Get the selected pair
            RequestReplyPair selected = (RequestReplyPair) lstOpenRequests.SelectedItem;
            currentPair = selected;

            // Display the pair
            lblDescription.Text = selected.Workrequest.Description;
            lblWorktype.Text = selected.Workrequest.Worktype;
            if (currentPair.hasResponse())
            {
                dtpDone.Value = currentPair.Workresponse.estimatedDone;
                txtNotes.Text = currentPair.Workresponse.Notes;
            }

            // Enable or disable send button depending on if this message was handled
            btnSend.Enabled = !currentPair.hasResponse();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Create a new workresponse
            WorkResponse resp = new WorkResponse();
            currentPair.Workresponse = resp;

            // Set its values
            resp.estimatedDone = dtpDone.Value;
            resp.Notes = txtNotes.Text;
            resp.Id = currentPair.Workrequest.Id;

            // Encode to json
            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(resp));
            IBasicProperties props = send_channel.CreateBasicProperties();

            // Add headers
            Dictionary<string, object> headers = new Dictionary<string, object>();
            props.Headers = headers;
            props.Headers.Add("reply_to", currentPair.Workrequest.ReturnAddress);
            props.Headers.Add("message_type", SEND_TYPE);
            props.ContentType = "text/plain";
            props.DeliveryMode = 2;

            // Send the message
            send_channel.BasicPublish(DEFAULT_EXCHANGE, ROUTING_KEY_SEND, props, messageBodyBytes);

            // Update the listboxes depending on open and closed requests
            this.Invoke((MethodInvoker)(() =>
            {
                lstOpenRequests.Items.Clear();
                lstClosedRequests.Items.Clear();
                foreach (RequestReplyPair val in messages)
                {
                    if (val.hasResponse())
                    {
                        lstClosedRequests.Items.Add(val);
                    } else
                    {
                        lstOpenRequests.Items.Add(val);
                    }

                }
                txtNotes.Text = "";
            }
            ));
        }

        private void ReplierForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Close the connection
            send_channel.Close(200, "Quit");
            recv_channel.Close(200, "Quit");
            connection.Close();
        }
    }
}
