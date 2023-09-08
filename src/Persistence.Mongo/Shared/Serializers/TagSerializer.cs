using MongoDB.Bson.Serialization;
using Searcher.Model.Shared.ValueObjects;
using System;

namespace Searcher.Persistence.Mongo.Shared.Serializers;

public sealed class TagSerializer : IBsonSerializer<Tag>
{
    public Type ValueType => typeof(Tag);

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is not Tag tag)
            throw new ArgumentException("Cannot cast value to 'Tag'.");

        Serialize(context, args, tag);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Tag value)
    {
        context.Writer.WriteStartDocument();
        context.Writer.WriteName("tag");
        context.Writer.WriteString(value.Value);
        context.Writer.WriteEndDocument();
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) =>
        Deserialize(context, args);

    public Tag Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        context.Reader.ReadStartDocument();
        var value = context.Reader.ReadString();
        context.Reader.ReadEndDocument();

        return new Tag(value);
    }
}
