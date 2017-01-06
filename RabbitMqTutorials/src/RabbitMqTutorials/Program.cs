using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMqTutorials.HelloWorld.Publish
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    Console.WriteLine("Enter text to send.  . to add 1 second sleep.  Enter blank line to end.");
                    var input = Console.ReadLine();
                    while (input != String.Empty)
                    {
                        var body = Encoding.UTF8.GetBytes(input);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "hello",
                                             basicProperties: properties,
                                             body: body);
                        Console.WriteLine(" [x] Sent {0}", input);
                        input = Console.ReadLine();
                    }
                    
                    
                }
            }
        }
    }
}
