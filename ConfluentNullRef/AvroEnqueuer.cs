using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.SchemaRegistry;

namespace ConfluentNullRef
{
    public class AvroEnqueuer:IDisposable
    {
        private ISchemaRegistryClient schemaClient;
        private IProducer<Guid, MessageTypes.LogMessage> producer;

        public void Open()
        {
            schemaClient = SchemaClientFactory.Create();
            producer = ProducerFactory.Create(schemaClient);
        }

        public async Task EnqueueAsync(MessageTypes.LogMessage msg)
        {
            var dr = await producer.ProduceAsync("test-topic", new Message<Guid, MessageTypes.LogMessage>()
            {
                Key = Guid.NewGuid(),
                Value = msg
            });

            if (dr.Status != PersistenceStatus.Persisted)
            {
                throw new InvalidOperationException("Message not persisted");
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.schemaClient?.Dispose();
                this.producer?.Dispose();
            }
        }
    }
}
