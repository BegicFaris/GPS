using GPS.API.Dtos.PasswordResetDtos;
using GPS.API.Dtos.TwoFactorDtos;
using GPS.API.Interfaces;
using GPS.API.Services.PasswordresetServices;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Get2FAStatus([FromQuery] string email, CancellationToken cancellationToken)
        {
            var twoFactorStatus = await _twoFactorAuthService.IsUsingTwoFactorAsync(email, cancellationToken);
            return Ok(new { twoFactorStatus });
        }

  
        [HttpPost("send-code")]
        public async Task<IActionResult> SendResetCode(EmailTwoFactorDto e, CancellationToken cancellationToken)
        {
            await _twoFactorAuthService.GenerateTwoFactorCodeAsync(e.Email, cancellationToken);
            return Ok();
        }

      
        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequestDto request, CancellationToken cancellationToken)
        {
            var isValid = await _twoFactorAuthService.VerifyTwoFactorCodeAsync(request.Email, request.Code, cancellationToken);
            return isValid ? Ok() : BadRequest("Invalid code")  ;
        }

    }

}