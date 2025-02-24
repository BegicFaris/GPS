using System.Text.Json;
using System.Configuration;

namespace GPS.API.Services.ReCaptchaService
{
    public class ReCaptchaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _secretKey;

        public ReCaptchaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _secretKey = configuration["ReCaptcha:SecretKey"]??"";
        }

        public async Task<bool> VerifyCaptcha(string token)
        {
            if(string.IsNullOrEmpty(_secretKey)) throw new ConfigurationErrorsException("Secret key not found!");
            var response = await _httpClient.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={_secretKey}&response={token}",
                null
            );

            if (!response.IsSuccessStatusCode) return false;

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ReCaptchaResponse>(jsonResponse);
            return result?.Success ?? false;
        }
        public class ReCaptchaResponse
        {
            public bool Success { get; set; }
            public string[]? Errors { get; set; } 
        }
    }
}
