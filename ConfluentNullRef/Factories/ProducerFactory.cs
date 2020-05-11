using System;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace ConfluentNullRef
{
    public static class ProducerFactory
    {
        public static IProducer<Guid, MessageTypes.LogMessage> Create(ISchemaRegistryClient schemaClient)
        {
            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092",
            };

            var builder = new ProducerBuilder<Guid, MessageTypes.LogMessage>(producerConfig)
                .SetKeySerializer(new GuidSerializer())
                .SetValueSerializer(new AvroSerializer<MessageTypes.LogMessage>(schemaClient).AsSyncOverAsync());

            return builder.Build();
        }
    }
}
