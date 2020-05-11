using Confluent.SchemaRegistry;

namespace ConfluentNullRef
{
    public static class SchemaClientFactory
    {
        public static ISchemaRegistryClient Create()
        {
            var schemaConfig = new SchemaRegistryConfig()
            {
                Url = "http://localhost:8081"
            };

            return new CachedSchemaRegistryClient(schemaConfig);
        }
    }
}
