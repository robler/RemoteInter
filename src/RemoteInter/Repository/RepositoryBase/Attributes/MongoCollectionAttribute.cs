namespace RemoteInter.Repository.RepositoryBase.Attributes;

public class MongoCollectionAttribute : Attribute
{
    public string? Name { get; set; }
}