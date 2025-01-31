using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.ShiftServices;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using Table = iText.Layout.Element.Table;
using iText.Layout.Properties;
using iText.IO.Image;

namespace GPS.API.Services.ShiftDetailServices
{
    public class ShiftDetailService(ApplicationDbContext context, IShiftService _shiftService) : IShiftDetailService
    {
        public async Task<IEnumerable<ShiftDetail>> GetAllShiftDetailsAsync() =>
     await context.ShiftDetails.Include(x => x.Shift).Include(x => x.Line).ToListAsync();

        public async Task<IEnumerable<ShiftDetail>> GetShiftDetailsByShiftIdAsync(int shiftId) =>
           await context.ShiftDetails.Include(x => x.Shift).Include(x => x.Line).Where(x => x.ShiftId == shiftId).ToListAsync();

        public async Task<ShiftDetail> GetShiftDetailByIdAsync(int id) =>
          await context.ShiftDetails.Include(x => x.Shift).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> DeleteShiftDetail(int id)
        {
            var shiftDetail = await context.ShiftDetails.FindAsync(id);
            if (shiftDetail == null) return false;
            context.ShiftDetails.Remove(shiftDetail);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteShiftDetailsByShiftId(int shiftId)
        {
            var shiftDetails = await context.ShiftDetails
                .Where(sd => sd.ShiftId == shiftId)
                .ToListAsync();
            if (shiftDetails == null || !shiftDetails.Any())
                return false;
            context.ShiftDetails.RemoveRange(shiftDetails);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<ShiftDetail> CreateShiftDetailAsync(ShiftDetail shiftDetail)
        {
            context.ShiftDetails.Add(shiftDetail);
            await context.SaveChangesAsync();
            return shiftDetail;
        }

        public async Task<byte[]> GeneratePdfAsync(int shiftId)
        {
            var shift = await _shiftService.GetShiftByIdAsync(shiftId);
            if (shift == null)
                throw new ArgumentException("Shift not found.");

            var shiftDetailList = await GetShiftDetailsByShiftIdAsync(shiftId);
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
                        .SetFont(headingFont).SetFontSize(16).SetTextAlignment(TextAlignment.CENTER));
                    document.Add(new Paragraph("Bišće polje bb")
                        .SetFont(bodyFont).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));
                    document.Add(new Paragraph("+387 (0)36 123-456")
                        .SetFont(bodyFont).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new Paragraph("\n"));

                    document.Add(new Paragraph("Shift Overview Report")
                        .SetFont(headingFont).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new Paragraph($"Driver: {shift.Driver.FirstName} {shift.Driver.LastName}")
                        .SetFont(bodyFont).SetFontSize(12));
                    document.Add(new Paragraph($"Bus: {shift.Bus.RegistrationNumber}")
                        .SetFont(bodyFont).SetFontSize(12));
                    document.Add(new Paragraph($"Date: {shift.ShiftDate:yyyy-MM-dd}")
                        .SetFont(bodyFont).SetFontSize(12));
                    document.Add(new Paragraph($"Duration: {shift.ShiftStartingTime:HH:mm} - {shift.ShiftEndingTime:HH:mm}")
                        .SetFont(bodyFont).SetFontSize(12));

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
                        table.AddCell(new Cell().Add(new Paragraph(s.Line.Name).SetFont(bodyFont)));
                        table.AddCell(new Cell().Add(new Paragraph(s.ShiftDetailStartingTime.ToShortTimeString()).SetFont(bodyFont)));
                        table.AddCell(new Cell().Add(new Paragraph(s.ShiftDetailEndingTime.ToShortTimeString()).SetFont(bodyFont)));
                    }

                    document.Add(table);
                    document.Add(new Paragraph("\n"));
                    document.Add(new Paragraph($"Page: {pdf.GetNumberOfPages()}")
                        .SetFont(bodyFont).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER));

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
