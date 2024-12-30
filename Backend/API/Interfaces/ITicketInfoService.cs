using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ITicketInfoService
    {
        Task<IEnumerable<TicketInfo>> GetAllAsync();
        Task<TicketInfo?> GetByIdAsync(int id);
        Task<TicketInfo> CreateAsync(TicketInfo ticketInfo);
        Task<TicketInfo> UpdateAsync(int id, TicketInfo ticketInfo);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TicketInfo>> GetByTicketTypeIdAsync(int ticketTypeId);
    }
}
