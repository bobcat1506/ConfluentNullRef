using System;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace ConfluentNullRef
{
    public static class ProducerFactory
    {
        public static IProducer<Guid, T> Create<T>(ISchemaRegistryClient schemaClient)
        {
            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092",
            };

            var builder = new ProducerBuilder<Guid, T>(producerConfig)
                .SetKeySerializer(new GuidSerializer())
                .SetValueSerializer(new AvroSerializer<T>(schemaClient).AsSyncOverAsync());

            return builder.Build();
        }
    }
}
