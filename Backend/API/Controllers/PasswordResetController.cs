// PasswordResetController.cs
using GPS.API.Controllers;
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

    public class emailDto
    {
        public string email { get; set; }
    }

    [HttpPost("send-code")]
    public async Task<IActionResult> SendResetCode(emailDto e)
    {
        await _passwordResetService.GenerateResetCode(e.email);
        return Ok();
    }

    [HttpPost("verify-code")]
    public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequest request)
    {
        var isValid = await _passwordResetService.VerifyResetCode(request.Email, request.Code);
        return isValid ? Ok() : BadRequest("Invalid code");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var success = await _passwordResetService.ResetPassword(request.Email, request.Code, request.NewPassword);
        return success ? Ok() : BadRequest("Unable to reset password");
    }
}

public class VerifyCodeRequest
{
    public string Email { get; set; }
    public string Code { get; set; }
}

public class ResetPasswordRequest
{
    public string Email { get; set; }
    public string Code { get; set; }
    public string NewPassword { get; set; }
}