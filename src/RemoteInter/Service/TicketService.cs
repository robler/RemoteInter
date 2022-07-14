using RemoteInter.Enum;
using RemoteInter.Model;
using RemoteInter.Model.Dao;
using RemoteInter.Repository;

namespace RemoteInter.Service;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task Create(TicketDao input)
    {
        await _ticketRepository.Create(input);
    }

    public async Task Update(TicketDao input)
    {
        await _ticketRepository.Update(input);
    }

    public async Task Delete(string id)
    {
        var ticket = await _ticketRepository.Get(id);
        ticket.IsDeleted = true;
        await _ticketRepository.Update(ticket);
    }

    public async Task<TicketDao> Get(string id)
    {
        return await _ticketRepository.Get(id);
    }

    public async Task<IEnumerable<TicketDao>> List(TicketType? type = null)
    {
        return await _ticketRepository.List(type);
    }

    public async Task Resolve(string id)
    {
        var ticket = await _ticketRepository.Get(id);
        ticket.Status = TicketStatus.Resloved;
        await _ticketRepository.Update(ticket);
    }
}