﻿namespace GPS.API.Dtos.TwoFactorDtos
{
    public class VerifyTwoFactorCodeRequestDto
    {
        public required string Email { get; set; }
        public required string Code { get; set; }
    }
}
