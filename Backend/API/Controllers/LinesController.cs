using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.FeedbackServices;

namespace GPS.API.Controllers
{
    public class LinesController : MyControllerBase
    {
        private readonly ILineService _lineService;

        public LinesController(ILineService lineService)
        {
            _lineService = lineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLines() =>
            Ok(await _lineService.GetAllLinesAsync());

        [HttpGet("station/{stationId}")]
        public async Task<IActionResult> GetAllLinesByStationId(int stationId) =>
            Ok(await _lineService.GetAllLinesByStationIdAsync(stationId));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLine(int id)
        {
            var line = await _lineService.GetLineByIdAsync(id);
            if (line == null) return NotFound();
            return Ok(line);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLine(Line line)
        {
            var createdLine = await _lineService.CreateLineAsync(line);
            return CreatedAtAction(nameof(GetLine), new { id = createdLine.Id }, createdLine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLine(int id, Line line)
        {
            if (id !=line.Id) return BadRequest();
            var updatedLine = await _lineService.UpdateLineAsync(line);
            return Ok(updatedLine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLine(int id)
        {
            var success = await _lineService.DeleteLineAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
