namespace GPS.API.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailWithPdfAsync(string email, string subject, string message, byte[] pdfBytes);
    }
}
