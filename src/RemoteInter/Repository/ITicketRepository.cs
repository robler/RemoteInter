using MongoDB.Bson;
using RemoteInter.Enum;
using RemoteInter.Model.Dao;

namespace RemoteInter.Repository
{
    public interface ITicketRepository : IMongoRepositoryBase<TicketDao, ObjectId>
    {
        public Task<TicketDao> Get(string id);

        public Task<IEnumerable<TicketDao>> List(TicketType? type);

        public Task Create(TicketDao input);

        public Task Update(TicketDao input);
    }
}