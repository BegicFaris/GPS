using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ITicketTypeService
    {
        Task<IEnumerable<TicketType>> GetAllAsync();
        Task<TicketType?> GetByIdAsync(int id);
        Task<TicketType> CreateAsync(TicketType ticketType);
        Task<TicketType?> UpdateAsync(int id, TicketType ticketType);
        Task<bool> DeleteAsync(int id);
    }
}
