using GPS.API.Dtos.PasswordResetDtos;
using GPS.API.Dtos.TwoFactorDtos;
using GPS.API.Interfaces;
using GPS.API.Services.PasswordresetServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GPS.API.Controllers
{
    public class TwoFactorAuthController : MyControllerBase
    {
        private readonly ITwoFactorAuthService _twoFactorAuthService;
        private readonly ILogger<TwoFactorAuthController> _logger;


        public TwoFactorAuthController(ITwoFactorAuthService twoFactorAuthService, ILogger<TwoFactorAuthController> logger)
        {
            _twoFactorAuthService = twoFactorAuthService;
            _logger = logger;
        }


        [HttpGet("get-2fa-status")]
        public async Task<IActionResult> Get2FAStatus([FromQuery] string email)
        {
            var twoFactorStatus = await _twoFactorAuthService.IsUsingTwoFactor(email);
            return Ok(new { twoFactorStatus });
        }


        [HttpPost("send-code")]
        public async Task<IActionResult> SendResetCode(emailTwoFactorDto e)
        {
            await _twoFactorAuthService.GenerateTwoFactorCode(e.email);
            return Ok();
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequestDto request)
        {
            var isValid = await _twoFactorAuthService.VerifyTwoFactorCode(request.Email, request.Code);
            return isValid ? Ok() : BadRequest("Invalid code")  ;
        }

    }

}