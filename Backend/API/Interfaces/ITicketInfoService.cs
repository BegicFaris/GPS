using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ITicketInfoService
    {
        Task<IEnumerable<TicketInfo>> GetAllAsync(CancellationToken cancellationToken);
        Task<TicketInfo?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<TicketInfo> CreateAsync(TicketInfo ticketInfo, CancellationToken cancellationToken);
        Task<TicketInfo> UpdateAsync(int id, TicketInfo ticketInfo, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<TicketInfo>> GetByTicketTypeIdAsync(int ticketTypeId, CancellationToken cancellationToken);
    }
}
