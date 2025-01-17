using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<object> GetTicketsOverTimeAsync();
        Task<object> GetUserTicketsPaginatedAsync(string email,int pageNumber, int pageSize);
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<List<Ticket>> GetAllTicketsForUserEmail(string email);
        Task<Ticket> CreateTicketAsync(Ticket ticket);
        Task<Ticket> UpdateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);
        Task<Ticket> CreateTicketOnBuying(int ticketInfoId, string userEmail);
    }
}
