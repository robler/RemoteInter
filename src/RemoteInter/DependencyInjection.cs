using MongoDB.Bson.Serialization.Conventions;
using RemoteInter.Repository.RepositoryBase;
using RemoteInter.Service;

namespace RemoteInter.Repository;

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddSingleton<ITicketRepository, TicketRepositoy>();

        return services;
    }

    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<ITicketService, TicketService>();

        return services;
    }

    public static IServiceCollection AddMongoDB(
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