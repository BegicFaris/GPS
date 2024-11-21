using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Dtos.LineDtos;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLine(int id)
        {
            var line = await _lineService.GetLineByIdAsync(id);
            if (line == null) return NotFound();
            return Ok(line);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLine(LineCreateDto lineCreateDto)
        {
            var line = new Line()
            {
                Name = lineCreateDto.Name,
                StartingStationId = lineCreateDto.StartingStationID,
                EndingStationId = lineCreateDto.EndingStationID,
                IsActive = lineCreateDto.IsActive,
                CompleteDistance = lineCreateDto.CompleteDistance,

            };
            var createdLine = await _lineService.CreateLineAsync(line);
            return CreatedAtAction(nameof(GetLine), new { id = createdLine.Id }, createdLine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLine(int id, LineUpdateDto lineUpdateDto)
        {
            if (id !=lineUpdateDto.Id) return BadRequest();

            var existingLine = await _lineService.GetLineByIdAsync(id);
            if (existingLine == null) return NotFound($"Line with Id:{id} not found!");

            if (!String.IsNullOrEmpty(lineUpdateDto.Name))
                existingLine.Name = lineUpdateDto.Name;
            if (lineUpdateDto.StartingStationID != null)
                existingLine.StartingStationId = lineUpdateDto.StartingStationID.Value;
            if (lineUpdateDto.EndingStationID != null)
                existingLine.EndingStationId = lineUpdateDto.EndingStationID.Value;
            if (lineUpdateDto.IsActive != null)
                existingLine.IsActive = lineUpdateDto.IsActive.Value;
            if (!String.IsNullOrEmpty(lineUpdateDto.CompleteDistance))
                existingLine.CompleteDistance = lineUpdateDto.CompleteDistance;

            var updatedLine = await _lineService.UpdateLineAsync(existingLine);
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
