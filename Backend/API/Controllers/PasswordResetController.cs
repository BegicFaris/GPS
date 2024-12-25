// PasswordResetController.cs
using GPS.API.Controllers;
using GPS.API.Dtos.PasswordResetDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

public class PasswordResetController : MyControllerBase
{
    private readonly IPasswordResetService _passwordResetService;

    public PasswordResetController(IPasswordResetService passwordResetService)
    {
        _passwordResetService = passwordResetService;
    }


    [HttpPost("send-code")]
    public async Task<IActionResult> SendResetCode(emailDto e)
    {
        await _passwordResetService.GenerateResetCode(e.email);
        return Ok();
    }

    [HttpPost("verify-code")]
    public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequestDto request)
    {
        var isValid = await _passwordResetService.VerifyResetCode(request.Email, request.Code);
        return isValid ? Ok() : BadRequest("Invalid code");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        var success = await _passwordResetService.ResetPassword(request.Email, request.Code, request.NewPassword);
        return success ? Ok() : BadRequest("Unable to reset password");
    }
}
