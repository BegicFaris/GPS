using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.ShiftServices;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.ShiftDetailServices
{
    public class ShiftDetailService(ApplicationDbContext context, IShiftService _shiftService) : IShiftDetailService
    {
        public async Task<IEnumerable<ShiftDetail>> GetAllShiftDetailsAsync(CancellationToken cancellationToken)
            => await context.ShiftDetails
                .Include(x => x.Shift)
                .Include(x => x.Line)
                .ToListAsync(cancellationToken);

        public async Task<IEnumerable<ShiftDetail>> GetShiftDetailsByShiftIdAsync(int shiftId, CancellationToken cancellationToken)
            => await context.ShiftDetails
                .Include(x => x.Shift)
                .Include(x => x.Line)
                .Where(x => x.ShiftId == shiftId)
                .ToListAsync(cancellationToken);

        public async Task<ShiftDetail?> GetShiftDetailByIdAsync(int id, CancellationToken cancellationToken)
            => await context.ShiftDetails
                .Include(x => x.Shift)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<bool> DeleteShiftDetail(int id, CancellationToken cancellationToken)
        {
            var shiftDetail = await context.ShiftDetails.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (shiftDetail == null)
                return false;
            context.ShiftDetails.Remove(shiftDetail);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteShiftDetailsByShiftId(int shiftId, CancellationToken cancellationToken)
        {
            var shiftDetails = await context.ShiftDetails
                .Where(sd => sd.ShiftId == shiftId)
                .ToListAsync(cancellationToken);
            if (shiftDetails == null || !shiftDetails.Any())
                return false;
            context.ShiftDetails.RemoveRange(shiftDetails);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IReadOnlyList<ShiftDetail>> CreateShiftDetailAsync(ShiftDetail[] shiftDetails, CancellationToken cancellationToken)
        {
            bool isValidTime = await IsValidTime(shiftDetails, cancellationToken);
            if (!isValidTime) throw new InvalidOperationException("Invalid shift detail time!");

            await context.ShiftDetails.AddRangeAsync(shiftDetails, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return shiftDetails;
        }

        private async Task<bool> IsValidTime(ShiftDetail[] shiftDetails, CancellationToken cancellationToken)
        {
            if (shiftDetails.Length == 0) return false;

            var shiftId = shiftDetails[0].ShiftId;

            for (int i = 1; i < shiftDetails.Length; i++)
                if (shiftId != shiftDetails[i].ShiftId) return false;


            var shift = await context.Shifts.SingleOrDefaultAsync(x => x.Id == shiftId, cancellationToken);
            if (shift == null) return false;

            ShiftDetail[] orderedShiftDetails = ShiftDetailsOrderByTime(shiftDetails);

            for (int i = 0; i < orderedShiftDetails.Length; i++)
            {
                if (i == 0)
                {
                    if (orderedShiftDetails[i].ShiftDetailStartingTime != shift.ShiftStartingTime) return false;
                }
                else
                {
                    if (orderedShiftDetails[i].ShiftDetailStartingTime < orderedShiftDetails[i-1].ShiftDetailEndingTime) return false;
                }
                if (i == orderedShiftDetails.Length - 1)
                    if (orderedShiftDetails[i].ShiftDetailEndingTime != shift.ShiftEndingTime) return false;
            }

            return true;

        }

        private ShiftDetail[] ShiftDetailsOrderByTime(ShiftDetail[] shiftDetails)
        {
            return shiftDetails.OrderBy(s => s.ShiftDetailStartingTime).ToArray();
        }

        public async Task<byte[]> GeneratePdfAsync(int shiftId, CancellationToken cancellationToken)
        {
            var shift = await _shiftService.GetShiftByIdAsync(shiftId, cancellationToken);
            if (shift == null)
                throw new ArgumentException("Shift not found.");

            var shiftDetailList = await GetShiftDetailsByShiftIdAsync(shiftId, cancellationToken);
            if (shiftDetailList == null || !shiftDetailList.Any())
                throw new ArgumentException("Shift details not found.");

            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(memoryStream);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    PdfFont headingFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    PdfFont bodyFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    string logoPath = "Resources\\GPSIcon.png"; // Update this path as needed
                    Image logo = new Image(ImageDataFactory.Create(logoPath))
                        .SetWidth(100f)
                        .SetHeight(50f)
                        .SetTextAlignment(TextAlignment.CENTER);

                    document.Add(logo);

                    document.Add(new Paragraph("Mostar bus")
                        .SetFont(headingFont)
                        .SetFontSize(16)
                        .SetTextAlignment(TextAlignment.CENTER));
                    document.Add(new Paragraph("Bišće polje bb")
                        .SetFont(bodyFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.CENTER));
                    document.Add(new Paragraph("+387 (0)36 123-456")
                        .SetFont(bodyFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new Paragraph("\n"));

                    document.Add(new Paragraph("Shift Overview Report")
                        .SetFont(headingFont)
                        .SetFontSize(18)
                        .SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new Paragraph($"Driver: {shift.Driver?.FirstName ?? ""} {shift.Driver?.LastName ?? ""}")
                        .SetFont(bodyFont)
                        .SetFontSize(12));
                    document.Add(new Paragraph($"Bus: {shift.Bus?.RegistrationNumber ?? ""}")
                        .SetFont(bodyFont)
                        .SetFontSize(12));
                    document.Add(new Paragraph($"Date: {shift.ShiftDate:yyyy-MM-dd}")
                        .SetFont(bodyFont)
                        .SetFontSize(12));
                    document.Add(new Paragraph($"Duration: {shift.ShiftStartingTime:HH:mm} - {shift.ShiftEndingTime:HH:mm}")
                        .SetFont(bodyFont)
                        .SetFontSize(12));

                    document.Add(new Paragraph("\n"));

                    Table table = new Table(3);
                    float pageWidth = pdf.GetDefaultPageSize().GetWidth();
                    table.SetWidth(pageWidth * 0.5f);
                    table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                    table.AddHeaderCell(new Cell().Add(new Paragraph("Line").SetFont(headingFont)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Start").SetFont(headingFont)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("End").SetFont(headingFont)));

                    foreach (var s in shiftDetailList)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(s.Line?.Name ?? "").SetFont(bodyFont)));
                        table.AddCell(new Cell().Add(new Paragraph(s.ShiftDetailStartingTime.ToShortTimeString()).SetFont(bodyFont)));
                        table.AddCell(new Cell().Add(new Paragraph(s.ShiftDetailEndingTime.ToShortTimeString()).SetFont(bodyFont)));
                    }

                    document.Add(table);
                    document.Add(new Paragraph("\n"));
                    document.Add(new Paragraph($"Page: {pdf.GetNumberOfPages()}")
                        .SetFont(bodyFont)
                        .SetFontSize(10)
                        .SetTextAlignment(TextAlignment.CENTER));

                    document.Close();

                    return memoryStream.ToArray();
                }
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while generating the PDF.");
            }
        }
    }
}
