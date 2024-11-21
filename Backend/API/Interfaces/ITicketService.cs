using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<Ticket> CreateTicketAsync(Ticket ticket);
        Task<Ticket> UpdateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);
    }
}
