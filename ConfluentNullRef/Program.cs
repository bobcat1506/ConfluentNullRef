using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Confluent.Kafka.Admin;

namespace ConfluentNullRef
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // after docker compose is running, create the topic by running this command:

            // docker exec -it broker kafka-topics --bootstrap-server localhost:9092 --create --topic test-topic

            using var schemaClient = SchemaClientFactory.Create();
            var schema = File.ReadAllText("ConfluentNullRef/LogMessage.avsc");
            var id = await schemaClient.RegisterSchemaAsync("LogMessage", schema);
            
            using var enqueuer = new AvroEnqueuer();
            await enqueuer.EnqueueAsync(new MessageTypes.LogMessage()
            {
                Message = "Hello",
                IP = "127.0.0.1",                     
                Severity = MessageTypes.LogLevel.Info,
                Tags = new Dictionary<string, string> {{ "key", "value"}}
            });
        }
    }
}
