using System.Text.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GPS.API.Services
{
    public class ReCaptchaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _secretKey;

        public ReCaptchaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _secretKey = configuration["ReCaptcha:SecretKey"];
        }

        public async Task<bool> VerifyCaptcha(string token)
        {
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
            public string[] Errors { get; set; }
        }
    }
}
