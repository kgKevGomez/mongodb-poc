using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace mongodb_poc
{
    public class NameSerializer : SerializerBase<Name>
    {
        public override Name Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new Name(context.Reader.ReadString());
        }

       
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Name value)
        {
            context.Writer.WriteString(value);
        }
    }
}
