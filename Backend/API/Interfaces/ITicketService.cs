using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync(CancellationToken cancellationToken, bool includeDeleted = false);
        Task<object> GetTicketsOverTimeAsync(CancellationToken cancellationToken);
        Task<Ticket?> GetTicketByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Ticket>> GetAllTicketsForUserEmail(string email, CancellationToken cancellationToken, bool includeDeleted = false);
        Task<Ticket> CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken);
        Task<Ticket> UpdateTicketAsync(Ticket ticket, CancellationToken cancellationToken);
        Task<bool> DeleteTicketAsync(int id, CancellationToken cancellationToken);
        Task<Ticket> CreateTicketOnBuying(int ticketInfoId, string userEmail, CancellationToken cancellationToken);
        Task<object> GetUserTicketsPaginatedAsync(string email, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
