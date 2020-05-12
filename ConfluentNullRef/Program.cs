using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Confluent.Kafka.Admin;
using io.confluent.examples.clients.basicavro;

namespace ConfluentNullRef
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // after docker compose is running, create the topic by running this command:

            // docker exec -it broker kafka-topics --bootstrap-server localhost:9092 --create --topic test-topic

            using var schemaClient = SchemaClientFactory.Create();

            // var schema = File.ReadAllText("ConfluentNullRef/LogMessage.avsc");
            // var id = await schemaClient.RegisterSchemaAsync("LogMessage", schema);

            // using var enqueuer = new AvroEnqueuer<MessageTypes.LogMessage>();
            // await enqueuer.EnqueueAsync(new MessageTypes.LogMessage()
            // {
            //     Message = "Hello",
            //     IP = "127.0.0.1",                     
            //     Severity = MessageTypes.LogLevel.Info,
            //     Tags = new Dictionary<string, string> {{ "key", "value"}}
            // });

            // simpler example, per mhowlett's suggestion
            var schema = File.ReadAllText("ConfluentNullRef/Payment.avsc");
            var id = await schemaClient.RegisterSchemaAsync("Payment", schema);

            using var enqueuer = new AvroEnqueuer<Payment>();
            await enqueuer.EnqueueAsync(new Payment()
            {
                id = Guid.NewGuid().ToString(),
                amount = 56.34
            });
        }
    }
}
