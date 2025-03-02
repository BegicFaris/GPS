using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Dtos.ShiftDtos;
using GPS.API.Interfaces;
using GPS.API.Services.TenantServices;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.ShiftServices
{
    public class ShiftService : IShiftService
    {
        private readonly ApplicationDbContext _context;

        public ShiftService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Shift>> GetAllShiftsAsync(CancellationToken cancellationToken, bool includeDeleted = false)
        {
            var query = _context.Shifts.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();
                var currentTenantId = _context.CurrentTenantID;
                if (string.IsNullOrEmpty(currentTenantId))
                {
                    return new List<Shift>();
                }
                query=query.Where(x=>x.TenantId == currentTenantId);
            }

            query = query.Include(x => x.Bus)
                         .Include(x => x.Driver);

            return await query.ToListAsync(cancellationToken);
        }




        public async Task<Shift?> GetShiftByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Shifts.Include(x => x.Bus).Include(x => x.Driver).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Shift> CreateShiftAsync(Shift shift, CancellationToken cancellationToken)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync(cancellationToken);
            return shift;
        }

        public async Task<Shift> UpdateShiftAsync(Shift shift, CancellationToken cancellationToken)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync(cancellationToken);
            return shift;
        }

        public async Task<bool> DeleteShiftAsync(int id, CancellationToken cancellationToken)
        {
            var shift = await _context.Shifts.FindAsync(new object[] { id }, cancellationToken);
            if (shift == null) return false;
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<ShiftDto>> GetDriverShiftsAsync(CancellationToken cancellationToken, int driverId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = _context.Shifts
                .Include(s => s.Bus)
                .Include(s => s.Driver)
                .Where(s => s.DriverId == driverId && !s.IsDeleted);

            if (fromDate.HasValue)
            {
                var from = DateOnly.FromDateTime(fromDate.Value);
                query = query.Where(s => s.ShiftDate >= from);
            }

            if (toDate.HasValue)
            {
                var to = DateOnly.FromDateTime(toDate.Value);
                query = query.Where(s => s.ShiftDate <= to);
            }

            var shifts = await query.OrderBy(s => s.ShiftDate).ThenBy(s => s.ShiftStartingTime).ToListAsync(cancellationToken);
            return shifts.Select(MapToShiftDTO);
        }

        public async Task<ShiftDetailsDto> GetShiftDetailsAsync(int shiftId, CancellationToken cancellationToken)
        {
            var shift = await _context.Shifts
                .Include(s => s.Bus)
                .Include(s => s.Driver)
                .FirstOrDefaultAsync(s => s.Id == shiftId && !s.IsDeleted, cancellationToken);

            if (shift == null)
                throw new KeyNotFoundException("Shift details not found.");

            var details = await _context.ShiftDetails
                .Include(sd => sd.Line)
                .Where(sd => sd.ShiftId == shiftId)
                .OrderBy(sd => sd.ShiftDetailStartingTime)
            .ToListAsync(cancellationToken);

            return new ShiftDetailsDto
            {
                Shift = MapToShiftDTO(shift),
                Details = details.Select(d => new ShiftDetailItemDTO
                {
                    Id = d.Id,
                    LineId = d.LineId,
                    LineName = d.Line?.Name ?? "Unknown",
                    StartTime = d.ShiftDetailStartingTime.ToTimeSpan(),
                    EndTime = d.ShiftDetailEndingTime.ToTimeSpan()
                }).ToList()
            };
        }

        public async Task<byte[]> GenerateShiftPdfAsync(int shiftId, CancellationToken cancellationToken)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var shiftDetails = await GetShiftDetailsAsync(shiftId, cancellationToken);
            if (shiftDetails == null)
                throw new KeyNotFoundException("Shift details not found.");

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    // Header with Logo and Title
                    page.Header().Row(row =>
                    {
                        row.RelativeItem(1).AlignLeft().Element(container =>
                        {
                            container.Image("./Resources/GPSIcon.png").FitArea(); // Adjust image height
                        });

                        row.RelativeItem(3).AlignRight().Text($"Shift Report - {shiftDetails.Shift?.ShiftDate:dd/MM/yyyy}")
                            .SemiBold().FontSize(18);
                    });

                    // Content Section
                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(col =>
                        {
                            col.Item().Text($"Driver: {shiftDetails.Shift?.DriverName}").FontSize(12);
                            col.Item().Text($"Bus: {shiftDetails.Shift?.BusNumber}").FontSize(12);
                            col.Item().Text($"Shift Time: {shiftDetails.Shift?.ShiftStartingTime:hh\\:mm} - {shiftDetails.Shift?.ShiftEndingTime:hh\\:mm}")
                                .FontSize(12);

                            // Table
                            col.Item().PaddingTop(10).Element(container =>
                            {
                                container.Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(3);
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(2);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().BorderBottom(1).Background("#EEEEEE").Text("Line").SemiBold();
                                        header.Cell().BorderBottom(1).Background("#EEEEEE").Text("Start Time").SemiBold();
                                        header.Cell().BorderBottom(1).Background("#EEEEEE").Text("End Time").SemiBold();
                                    });

                                    foreach (var detail in shiftDetails.Details)
                                    {
                                        table.Cell().Text(detail.LineName);
                                        table.Cell().Text($"{detail.StartTime:hh\\:mm}");
                                        table.Cell().Text($"{detail.EndTime:hh\\:mm}");
                                    }
                                });
                            });
                        });

                    // Footer
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                            x.Span(" of ");
                            x.TotalPages();
                        });
                });
            });
            cancellationToken.ThrowIfCancellationRequested();
            return document.GeneratePdf();
        }



        public async Task<IEnumerable<ShiftDto>> GetCurrentShiftsAsync(int driverId, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var now = TimeOnly.FromDateTime(DateTime.Now);

            var shifts = await _context.Shifts
                .Include(s => s.Bus)
                .Include(s => s.Driver)
                .Where(s => s.DriverId == driverId &&
                           !s.IsDeleted &&
                           s.ShiftDate == today &&
                           s.ShiftStartingTime <= now &&
                           s.ShiftEndingTime >= now)
                .ToListAsync(cancellationToken);

            return shifts.Select(MapToShiftDTO);
        }

        public async Task<IEnumerable<ShiftDto>> GetUpcomingShiftsAsync(int driverId, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var now = TimeOnly.FromDateTime(DateTime.Now);

            var shifts = await _context.Shifts
                .Include(s => s.Bus)
                .Include(s => s.Driver)
                .Where(s => s.DriverId == driverId &&
                           !s.IsDeleted &&
                           (s.ShiftDate > today ||
                            (s.ShiftDate == today && s.ShiftStartingTime > now)))
                .OrderBy(s => s.ShiftDate)
                .ThenBy(s => s.ShiftStartingTime)
                .ToListAsync(cancellationToken);

            return shifts.Select(MapToShiftDTO);
        }

        public async Task<IEnumerable<ShiftDto>> GetEndedShiftsAsync(int driverId, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var now = TimeOnly.FromDateTime(DateTime.Now);

            var shifts = await _context.Shifts
                .Include(s => s.Bus)
                .Include(s => s.Driver)
                .Where(s => s.DriverId == driverId &&
                           !s.IsDeleted &&
                           (s.ShiftDate < today ||
                            (s.ShiftDate == today && s.ShiftEndingTime < now)))
                .OrderByDescending(s => s.ShiftDate)
                .ThenByDescending(s => s.ShiftEndingTime)
                .ToListAsync(cancellationToken);

            return shifts.Select(MapToShiftDTO);
        }

        private ShiftDto MapToShiftDTO(Shift shift)
        {
            var now = DateTime.Now;
            var shiftDate = shift.ShiftDate.ToDateTime(TimeOnly.MinValue);
            var startDateTime = shiftDate.Add(shift.ShiftStartingTime.ToTimeSpan());
            var endDateTime = shiftDate.Add(shift.ShiftEndingTime.ToTimeSpan());

            string status;
            if (now > endDateTime)
                status = "Ended";
            else if (now >= startDateTime && now <= endDateTime)
                status = "Current";
            else
                status = "Upcoming";

            return new ShiftDto
            {
                Id = shift.Id,
                BusId = shift.BusId,
                BusNumber = shift.Bus?.RegistrationNumber,
                DriverId = shift.DriverId,
                DriverName = $"{shift.Driver?.FirstName} {shift.Driver?.LastName}" ?? "Unknown",
                ShiftDate = shiftDate,
                ShiftStartingTime = shift.ShiftStartingTime.ToTimeSpan(),
                ShiftEndingTime = shift.ShiftEndingTime.ToTimeSpan(),
                Status = status
            };
        }
    }
}
