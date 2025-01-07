using GPS.API.Data.Models;
using GPS.API.Dtos.FavoriteLineDtos;
using GPS.API.Dtos.ShiftDetailDtos;
using GPS.API.Interfaces;
using GPS.API.Services.FavoriteLineService;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class ShiftDetailsController(IShiftDetailService shiftDetailService) : MyControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllShiftDetails() =>
          Ok(await shiftDetailService.GetAllShiftDetailsAsync());

        [HttpGet("shift/{shiftId}")]
        public async Task<IActionResult> GetAllShiftDetailsBYShiftId(int shiftId)
        {
            var favoriteLines = await shiftDetailService.GetShiftDetailsByShiftIdAsync(shiftId);
            if (favoriteLines == null) return NotFound();
            return Ok(favoriteLines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShiftDetailById(int id)
        {
            var shiftDetail = await shiftDetailService.GetShiftDetailByIdAsync(id);
            if (shiftDetail == null) return NotFound();
            return Ok(shiftDetail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShiftDetail(ShiftDetailCreateDto shiftDetailCreateDto)
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
            var createdShiftDetail = await shiftDetailService.CreateShiftDetailAsync(shiftDetail);


            return Ok(createdShiftDetail);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftDetail(int id)
        {
            var success = await shiftDetailService.DeleteShiftDetail(id);
            if (!success) return NotFound();
            return NoContent();
        }
        [HttpDelete("shift/{shiftId}")]
        public async Task<IActionResult> DeleteShiftDetailsByShiftId(int shiftId)
        {
            var success = await shiftDetailService.DeleteShiftDetailsByShiftId(shiftId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
