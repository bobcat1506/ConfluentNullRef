using System;
using System.Text;
using Confluent.Kafka;

namespace ConfluentNullRef
{
    public class GuidSerializer : ISerializer<Guid>
    {
        public byte[] Serialize(Guid data, SerializationContext context)
        {
            var value = data.ToString();
            return Encoding.UTF8.GetBytes(value);
        }
    }
}
