using MongoDB.Bson.Serialization.Attributes;

namespace RemoteInter.Repository.RepositoryBase;

public interface IDocument<TKey>
{
    [BsonId]
    TKey Id { get; set; }
}