using GPS.API.Data.Models;
using GPS.API.Dtos.ShiftDetailDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using Table = iText.Layout.Element.Table;
using iText.Layout.Properties;
using iText.IO.Image;
using GPS.API.Services.ShiftDetailServices;
using Microsoft.AspNetCore.Authorization;

namespace GPS.API.Controllers
{
    public class ShiftDetailsController(IShiftDetailService shiftDetailService, IShiftService shiftService) : MyControllerBase
    {
        [Authorize(Roles = $"{nameof(UserRole.Manager)},{nameof(UserRole.Driver)}")]
        [HttpGet]
        public async Task<IActionResult> GetAllShiftDetails(CancellationToken cancellationToken) =>
          Ok(await shiftDetailService.GetAllShiftDetailsAsync(cancellationToken));

        [Authorize(Roles = $"{nameof(UserRole.Manager)},{nameof(UserRole.Driver)}")]
        [HttpGet("shift/{shiftId}")]
        public async Task<IActionResult> GetAllShiftDetailsBYShiftId(int shiftId, CancellationToken cancellationToken)
        {
            var favoriteLines = await shiftDetailService.GetShiftDetailsByShiftIdAsync(shiftId,cancellationToken);
            if (favoriteLines == null) return NotFound();
            return Ok(favoriteLines);
        }


        [Authorize(Roles = $"{nameof(UserRole.Manager)},{nameof(UserRole.Driver)}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShiftDetailById(int id, CancellationToken cancellationToken)
        {
            var shiftDetail = await shiftDetailService.GetShiftDetailByIdAsync(id, cancellationToken);
            if (shiftDetail == null) return NotFound();
            return Ok(shiftDetail);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateShiftDetail(ShiftDetailCreateDto shiftDetailCreateDto, CancellationToken cancellationToken)
        {

            if (!TimeOnly.TryParse(shiftDetailCreateDto.ShiftDetailStartingTime, out var shiftDetailStartingTime))
            {
                return BadRequest(new { message = "Invalid starting time format." });
            }
            if (!TimeOnly.TryParse(shiftDetailCreateDto.ShiftDetailEndingTime, out var shiftDetailEndingTime))
            {
                return BadRequest(new { message = "Invalid ending time format." });
            }
            var shiftDetail = new ShiftDetail
            {
                ShiftId = shiftDetailCreateDto.ShiftId,
                LineId = shiftDetailCreateDto.LineId,
                ShiftDetailStartingTime = shiftDetailStartingTime,
                ShiftDetailEndingTime = shiftDetailEndingTime,
            };
            var createdShiftDetail = await shiftDetailService.CreateShiftDetailAsync(shiftDetail, cancellationToken);


            return Ok(createdShiftDetail);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftDetail(int id, CancellationToken cancellationToken)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;
            if (role != UserRole.Manager.ToString())
                return Unauthorized();

            var success = await shiftDetailService.DeleteShiftDetail(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("shift/{shiftId}")]
        public async Task<IActionResult> DeleteShiftDetailsByShiftId(int shiftId, CancellationToken cancellationToken)
        {
 

            var success = await shiftDetailService.DeleteShiftDetailsByShiftId(shiftId, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }

        

        [Authorize(Roles = $"{nameof(UserRole.Manager)},{nameof(UserRole.Driver)}")]
        [HttpGet("generate-pdf/{shiftId}")]
        public async Task<IActionResult> GeneratePdf(int shiftId, CancellationToken cancellationToken)
        {
            var shift = await shiftService.GetShiftByIdAsync(shiftId, cancellationToken);
            if (shift == null)
                return NotFound(new { message = "Shift not found." });

          
            var shiftDetailList = await shiftDetailService.GetShiftDetailsByShiftIdAsync(shiftId, cancellationToken);
            if (shiftDetailList == null || !shiftDetailList.Any())
                return NotFound(new { message = "Shift details not found." });
            try
            {
                var pdfBytes = await shiftDetailService.GeneratePdfAsync(shiftId, cancellationToken);
                return File(pdfBytes, "application/pdf", "ShiftOverviewReport.pdf");
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while generating the PDF." });
            }



            //try
            //{
            //    using (MemoryStream memoryStream = new MemoryStream())
            //    {
            //        PdfWriter writer = new PdfWriter(memoryStream);
            //        PdfDocument pdf = new PdfDocument(writer);
            //        Document document = new Document(pdf);

            //        PdfFont headingFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            //        PdfFont bodyFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            //        string logoPath = "Resources\\GPSIcon.png"; // Replace with the correct path to the logo
            //        Image logo = new Image(ImageDataFactory.Create(logoPath));
            //        logo.SetWidth(100f); // Set logo width, adjust as needed
            //        logo.SetHeight(50f); // Set logo height, adjust as needed
            //        document.Add(logo.SetTextAlignment(TextAlignment.CENTER)); // Center the logo

            //        // Add company details (name, address, etc.)
            //        document.Add(new Paragraph("Mostar bus")
            //            .SetFont(headingFont).SetFontSize(16).SetTextAlignment(TextAlignment.CENTER));
            //        document.Add(new Paragraph("Bišće polje bb")
            //            .SetFont(bodyFont).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));
            //        document.Add(new Paragraph("+387 (0)36 123-456")
            //            .SetFont(bodyFont).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));

            //        document.Add(new Paragraph("\n"));

            //        // Add Shift Overview Title
            //        document.Add(new Paragraph("Shift Overview Report")
            //            .SetFont(headingFont).SetFontSize(18).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

            //        // Add Shift Details
            //        document.Add(new Paragraph($"Driver: {shift.Driver.FirstName} {shift.Driver.LastName}")
            //            .SetFont(bodyFont).SetFontSize(12));
            //        document.Add(new Paragraph($"Bus: {shift.Bus.RegistrationNumber}")
            //            .SetFont(bodyFont).SetFontSize(12));
            //        document.Add(new Paragraph($"Date: {shift.ShiftDate:yyyy-MM-dd}")
            //            .SetFont(bodyFont).SetFontSize(12));
            //        document.Add(new Paragraph($"Duration: {shift.ShiftStartingTime:HH:mm} - {shift.ShiftEndingTime:HH:mm}")
            //            .SetFont(bodyFont).SetFontSize(12));

            //        document.Add(new Paragraph("\n")); // Space between shift details and table

            //        // Create a Table for Shift Details (Line, Start, End) - 3 columns
            //        Table table = new Table(3);

            //        // Set table width percentage to 70% (wider table)
            //        float pageWidth = pdf.GetDefaultPageSize().GetWidth();
            //        float tableWidth = pageWidth * 0.5f; // 50% of the page width

            //        // Set column widths as equal parts of the table's total width
            //        table.SetWidth(tableWidth);

            //        // Set table header cells with bold text
            //        table.AddHeaderCell(new Cell().Add(new Paragraph("Line").SetFont(headingFont)));
            //        table.AddHeaderCell(new Cell().Add(new Paragraph("Start").SetFont(headingFont)));
            //        table.AddHeaderCell(new Cell().Add(new Paragraph("End").SetFont(headingFont)));

            //        // Add shift details to the table
            //        foreach (var s in shiftDetailList)
            //        {
            //            table.AddCell(new Cell().Add(new Paragraph(s.Line.Name).SetFont(bodyFont)));
            //            table.AddCell(new Cell().Add(new Paragraph(s.ShiftDetailStartingTime.ToShortTimeString()).SetFont(bodyFont)));
            //            table.AddCell(new Cell().Add(new Paragraph(s.ShiftDetailEndingTime.ToShortTimeString()).SetFont(bodyFont)));
            //        }

            //        // Center the table on the page
            //        table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            //        // Add table to document
            //        document.Add(table);

            //        // Add footer with page number
            //        document.Add(new Paragraph("\n"));
            //        document.Add(new Paragraph($"Page: {pdf.GetNumberOfPages()}")
            //            .SetFont(bodyFont).SetFontSize(10).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

            //        // Close the document to finalize the PDF
            //        document.Close();

            //        // Return the PDF as a downloadable file
            //        return File(memoryStream.ToArray(), "application/pdf", "ShiftOverviewReport.pdf");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception (optional)
            //    // _logger.LogError(ex, "Error generating PDF");

            //    return StatusCode(500, new { message = "An error occurred while generating the PDF." });
            //}
        }
    }
}
