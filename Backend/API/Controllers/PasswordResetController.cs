// PasswordResetController.cs
using GPS.API.Controllers;
using GPS.API.Dtos.PasswordResetDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GPS.API.Controllers
{


    public class PasswordResetController(IPasswordResetService passwordResetService) : MyControllerBase
    {
        private readonly IPasswordResetService _passwordResetService = passwordResetService;

        [HttpPost("send-code")]
        public async Task<IActionResult> SendResetCode(EmailDto e, CancellationToken cancellationToken)
        {
            await _passwordResetService.GenerateResetCode(e.Email, cancellationToken);
            return Ok();
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequestDto request, CancellationToken cancellationToken)
        {
            var isValid = await _passwordResetService.VerifyResetCode(request.Email, request.Code, cancellationToken);
            return isValid ? Ok() : BadRequest("Invalid code");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request, CancellationToken cancellationToken)
        {
            var success = await _passwordResetService.ResetPassword(request.Email, request.Code, request.NewPassword, cancellationToken);
            return success ? Ok() : BadRequest("Unable to reset password");
        }
    }
}