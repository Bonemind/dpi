require 'bunny'
require 'json'
require 'pp'

# Host to connect to
HOST = 'localhost'

# The queue of the requests we have to process
GENERAL_QUEUE = 'general_queue'

# The queue of workrequests
REQUEST_PROCESSOR_QUEUE = 'request_processor_queue'

# Connect to RabbitMQ
rmq = Bunny.new(hostname: 'localhost')
rmq.start

# Create a channel
channel = rmq.create_channel

# Create queue for the messages that need routing
general_queue = channel.queue(GENERAL_QUEUE)

# Create a queue for the routed request messages
request_processor_queue = channel.queue(REQUEST_PROCESSOR_QUEUE)


# Process all messages in the general queue in a loop
general_queue.subscribe(block: true) do |delivery_info, props, body|
	# Get the message type and delivery address
	msg_type = props[:headers]['message_type']

	# Logging
	puts "Recieved type: #{msg_type}"
	puts "Content: "
	pp JSON.parse(body)

	# We recieved a workrequest, route this to the general workrequest queue
	if msg_type == 'WORKREQUEST'
		puts "Routing to #{REQUEST_PROCESSOR_QUEUE}"

		# Forward the message to the workrequest queue
		request_processor_queue.publish(body, routing_key: request_processor_queue.name, headers: props[:headers])
	elsif msg_type == 'WORKRESPONSE'
		# We recieved a workresponse message
		# Fetch the reply address and route the message to that queue
		reply_to = props[:headers]['reply_to']
		puts "Routing to #{reply_to}"

		channel.queue(reply_to).publish(body, routing_key: reply_to, headers: props[:headers])
	else
		# We got and unknowd message, log then drop
		puts "Invalid message type #{message_type}. Ignoring"
	end
end



