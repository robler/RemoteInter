using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RemoteInter.Enum;
using RemoteInter.Model.Dao;
using RemoteInter.Repository.RepositoryBase;

namespace RemoteInter.Repository;

public class TicketRepositoy : MongoRepositoryBase<TicketDao, ObjectId>, ITicketRepository
{
    private readonly ILogger _logger;

    public TicketRepositoy(
        IOptions<List<MongoRepositoryOption>> options,
        ILogger<TicketRepositoy> logger) : base(options)
    {
        _logger = logger;
    }

    public async Task<TicketDao?> Get(string id)
    {
        var builder = Builders<TicketDao>.Filter;
        var filter = builder.Empty;

        filter &= builder.Eq(x => x.Id, ObjectId.Parse(id));

        return await FindOneAsync(filter);
    }

    public async Task<IEnumerable<TicketDao>> List(TicketType? type)
    {
        var builder = Builders<TicketDao>.Filter;
        var filter = builder.Empty;

        if (type != null)
            filter &= builder.Eq(x => x.Type, type);

        filter &= builder.Eq(x => x.IsDeleted, false);

        return await FindAsync(filter);
    }

    public async Task Create(TicketDao input)
    {
        await Collection.InsertOneAsync(input);
    }

    public async Task Update(TicketDao input)
    {
        await ReplaceAsync(input);
    }
}