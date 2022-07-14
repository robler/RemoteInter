using RemoteInter.Enum;
using RemoteInter.Model.Dao;

namespace RemoteInter.Service;

public interface ITicketService
{
    public Task<TicketDao> Get(string id);

    public Task<IEnumerable<TicketDao>> List(TicketType? type = null);

    public Task Create(TicketDao input);

    public Task Update(TicketDao input);

    public Task Delete(string id);

    public Task Resolve(string id);
}