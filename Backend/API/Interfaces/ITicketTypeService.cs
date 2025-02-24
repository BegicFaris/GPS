using GPS.API.Data.Models;


namespace GPS.API.Interfaces
{
    public interface ITicketTypeService
    {
        Task<IEnumerable<TicketType>> GetAllAsync(CancellationToken cancellationToken);
        Task<TicketType?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<TicketType> CreateAsync(TicketType ticketType, CancellationToken cancellationToken);
        Task<TicketType> UpdateAsync(int id, TicketType ticketType, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
