using Microsoft.AspNetCore.Mvc;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Helper.Api;
using RS1_2024_25.API.Interfaces;

namespace RS1_2024_25.API.Controllers
{
    public class LinesController: MyEndpointBase
    {
        private readonly ILineService _lineService;

        public LinesController(ILineService lineService)
        {
            _lineService = lineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLines() =>
            Ok(await _lineService.GetAllLinesAsync());

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
            if (id != line.Id) return BadRequest();
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
