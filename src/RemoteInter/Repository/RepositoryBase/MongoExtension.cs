using MongoDB.Bson.Serialization.Conventions;

namespace RemoteInter.Repository.RepositoryBase;

public static class MongoExtension
{
    public static IServiceCollection AddMongoDBRepository(
        this IServiceCollection services,
        IConfiguration config
        )
    {
        var camelCaseConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConventionPack, type => true);

        services.Configure<List<MongoRepositoryOption>>(config);
        return services;
    }
}