using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using Table = iText.Layout.Element.Table;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Layout.Borders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using Route = GPS.API.Data.Models.Route;
using Microsoft.AspNetCore.Authorization;

namespace GPS.API.Controllers
{
    public class LinesController : MyControllerBase
    {
        private readonly ILineService _lineService;
        private readonly IScheduleService _scheduleService;
        private readonly IRouteService _routeService;

        public LinesController(ILineService lineService, IScheduleService scheduleService, IRouteService routeService)
        {
            _lineService = lineService;
            _scheduleService = scheduleService;
            _routeService = routeService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllLines([FromQuery] string? lineName=null, [FromQuery] string? stationName=null, CancellationToken cancellationToken=default)
        {
            return Ok(await _lineService.GetAllLinesAsync(lineName, stationName,cancellationToken));
        }

        [HttpGet("station/{stationId}")]
        public async Task<IActionResult> GetAllLinesByStationId(int stationId, CancellationToken cancellationToken) =>
            Ok(await _lineService.GetAllLinesByStationIdAsync(stationId, cancellationToken));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLine(int id, CancellationToken cancellationToken)
        {
            var line = await _lineService.GetLineByIdAsync(id, cancellationToken);
            if (line == null) return NotFound();
            return Ok(line);
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateLine(Line line, CancellationToken cancellationToken)
        {
            var createdLine = await _lineService.CreateLineAsync(line, cancellationToken);
            return CreatedAtAction(nameof(GetLine), new { id = createdLine.Id }, createdLine);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLine(int id, Line line, CancellationToken cancellationToken)
        {
            if (id != line.Id) return BadRequest();
            var updatedLine = await _lineService.UpdateLineAsync(line, cancellationToken);
            return Ok(updatedLine);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLine(int id, CancellationToken cancellationToken)
        {
            var success = await _lineService.DeleteLineAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpGet("generate-pdf")]
        public async Task<IActionResult> GeneratePdf(CancellationToken cancellationToken)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(memoryStream);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    PdfFont headingFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    PdfFont bodyFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    // Create a table for logo and company details in the same row
                    Table logoInfoTable = new Table(2); // 2 columns: one for logo, one for company details
                    logoInfoTable.SetWidth(UnitValue.CreatePercentValue(100));  // Ensure it takes up 100% width
                    logoInfoTable.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                    // Add the logo (adjust path as needed)
                    string logoPath = "Resources\\GPSIcon.png"; // Replace with the correct path to the logo
                    Image logo = new Image(ImageDataFactory.Create(logoPath)).SetWidth(100f).SetHeight(50f);
                    logoInfoTable.AddCell(new Cell().Add(logo).SetBorder(Border.NO_BORDER));

                    // Add company details
                    var companyDetails = new Paragraph(NormalizeText("Mostar bus\nBisce polje bb\n+387 (0)36 123-456"))
                        .SetFont(bodyFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.LEFT);
                    logoInfoTable.AddCell(new Cell().Add(companyDetails).SetBorder(Border.NO_BORDER));

                    document.Add(logoInfoTable);
                    document.Add(new Paragraph("\n")); // Space between logo and main content

                    // Add title for the document
                    document.Add(new Paragraph(NormalizeText("Line Information Report"))
                        .SetFont(headingFont).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));

                    var lineList = await _lineService.GetAllLinesAsync(null,null,cancellationToken);

                    // Create a table with 2 columns per row: one for line names, one for details (routes & times)
                    Table lineTable = new Table(2); // 2 columns
                    lineTable.SetWidth(UnitValue.CreatePercentValue(100));  // Ensure the table occupies the entire page width
                    lineTable.SetHorizontalAlignment(HorizontalAlignment.LEFT);

                    var lineListIndexed = lineList.ToList();
                    // Loop through lines in pairs (Line 1, Line 2)
                    for (int i = 0; i < lineListIndexed.Count; i += 2)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            return StatusCode(499,"Cancelled by user");


                        var line1 = lineListIndexed[i];
                        var line2 = i + 1 < lineListIndexed.Count ? lineListIndexed[i + 1] : null;

                        // Add Row for Line Names (Both columns)
                        lineTable.AddCell(new Cell().Add(new Paragraph(NormalizeText($"Line: {line1.Name}"))
                            .SetFont(headingFont).SetFontSize(14)));
                        lineTable.AddCell(new Cell().Add(new Paragraph(NormalizeText(line2 != null ? $"Line: {line2.Name}" : string.Empty))
                            .SetFont(headingFont).SetFontSize(14)));

                        // Add "Route" header row
                        lineTable.AddCell(new Cell().Add(new Paragraph(NormalizeText("Route"))
                            .SetFont(headingFont).SetFontSize(12)));
                        lineTable.AddCell(new Cell().Add(new Paragraph(NormalizeText("Route"))
                            .SetFont(headingFont).SetFontSize(12)));

                        // Add routes for Line 1
                        var routes1 = await _routeService.GetAllRoutesByLineIdAsync(line1.Id,cancellationToken);
                        string routeString1 = string.Join(" | ", routes1.Select(r => NormalizeText(r.Station?.Name??"")));
                        var routeLines1 = SplitTextIntoMultipleLines(routeString1, 70);

                        // Add routes for Line 2
                        List<Route> routes2 = new List<Route>();
                        if (line2 != null)
                        {
                            routes2 = (await _routeService.GetAllRoutesByLineIdAsync(line2.Id, cancellationToken)).ToList();
                        }
                        string routeString2 = string.Join(" | ", routes2.Select(r => NormalizeText(r.Station?.Name ?? "")));
                        var routeLines2 = SplitTextIntoMultipleLines(routeString2, 70);

                        // Add routes in columns, side-by-side for Line 1 and Line 2
                        for (int j = 0; j < Math.Max(routeLines1.Count(), routeLines2.Count()); j++)
                        {
                            if (cancellationToken.IsCancellationRequested)
                                return StatusCode(499, "Cancelled by user");

                            lineTable.AddCell(new Cell().Add(new Paragraph(j < routeLines1.Count() ? routeLines1[j] : string.Empty)
                                .SetFont(bodyFont).SetFontSize(12)));
                            lineTable.AddCell(new Cell().Add(new Paragraph(j < routeLines2.Count() ? routeLines2[j] : string.Empty)
                                .SetFont(bodyFont).SetFontSize(12)));
                        }

                        // Add "Departure Times" header row
                        lineTable.AddCell(new Cell().Add(new Paragraph(NormalizeText("Departure Times"))
                            .SetFont(headingFont).SetFontSize(12)));
                        lineTable.AddCell(new Cell().Add(new Paragraph(NormalizeText("Departure Times"))
                            .SetFont(headingFont).SetFontSize(12)));

                        // Add departure times for Line 1
                        var schedules1 = await _scheduleService.GetAllSchedulesByLineIdAsync(line1.Id, cancellationToken);
                        string departureTimesString1 = string.Join(" | ", schedules1.Select(s => NormalizeText(s.DepartureTime.ToString("HH:mm"))));
                        var departureTimes1 = SplitTextIntoMultipleLines(departureTimesString1, 70);

                        // Add departure times for Line 2
                        List<Schedule> schedules2 = new List<Schedule>();
                        if (line2 != null)
                        {
                            schedules2 = (await _scheduleService.GetAllSchedulesByLineIdAsync(line2.Id, cancellationToken)).ToList();
                        }
                        string departureTimesString2 = string.Join(" | ", schedules2.Select(s => NormalizeText(s.DepartureTime.ToString("HH:mm"))));
                        var departureTimes2 = SplitTextIntoMultipleLines(departureTimesString2, 70);

                        // Add departure times in columns, side-by-side for Line 1 and Line 2
                        for (int j = 0; j < Math.Max(departureTimes1.Count(), departureTimes2.Count()); j++)
                        {
                            if (cancellationToken.IsCancellationRequested)
                                return StatusCode(499, "Cancelled by user");

                            lineTable.AddCell(new Cell().Add(new Paragraph(j < departureTimes1.Count() ? departureTimes1[j] : string.Empty)
                                .SetFont(bodyFont).SetFontSize(12)));
                            lineTable.AddCell(new Cell().Add(new Paragraph(j < departureTimes2.Count() ? departureTimes2[j] : string.Empty)
                                .SetFont(bodyFont).SetFontSize(12)));
                        }

                        // Add an empty row for separation between line pairs (if needed)
                        lineTable.AddCell(new Cell().Add(new Paragraph("\n")).SetBorder(Border.NO_BORDER));
                        lineTable.AddCell(new Cell().Add(new Paragraph("\n")).SetBorder(Border.NO_BORDER));
                    }

                    // Add the table to the document
                    document.Add(lineTable);

                    // Close the document (this is essential to finalize the PDF)
                    document.Close();

                    // Return the PDF file as a downloadable file
                    return File(memoryStream.ToArray(), "application/pdf", "LineInformationReport.pdf");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "Error generating PDF");

                return StatusCode(500, new { message = $"An error occurred while generating the PDF. Exception: {ex}" });
            }
        }
        private List<string> SplitTextIntoMultipleLines(string text, int maxLength)
        {
            var result = new List<string>();
            var currentLine = new StringBuilder();

            foreach (var word in text.Split(' '))
            {
                if (currentLine.Length + word.Length + 1 > maxLength)
                {
                    result.Add(currentLine.ToString());
                    currentLine.Clear();
                }

                if (currentLine.Length > 0)
                    currentLine.Append(" ");

                currentLine.Append(word);
            }

            if (currentLine.Length > 0)
                result.Add(currentLine.ToString());

            return result;
        }
        private string NormalizeText(string input)
        {
            // Replace special characters with their normalized versions
            input = input.Replace("č", "c")
                         .Replace("ć", "c")
                         .Replace("š", "s")
                         .Replace("ž", "z")
                         .Replace("đ", "d")
                         .Replace("Č", "C")
                         .Replace("Ć", "C")
                         .Replace("Š", "S")
                         .Replace("Ž", "Z")
                         .Replace("Đ", "D");

            return input;
        }
    }
}
