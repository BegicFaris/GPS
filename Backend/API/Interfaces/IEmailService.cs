namespace GPS.API.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken);
        Task SendEmailWithPdfAsync(string email, string subject, string message, byte[] pdfBytes, CancellationToken cancellationToken);
    }
}
