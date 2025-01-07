using System.Net;
using System.Net.Mail;
using DiscoverYourself.Models.MailModels;
using Microsoft.Extensions.Options;

namespace DiscoverYourself.Services;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var smtpClient = new SmtpClient(_mailSettings.SmtpServer)
        {
            Port = _mailSettings.Port,
            Credentials = new NetworkCredential(_mailSettings.SenderEmail, _mailSettings.SenderPassword),
            EnableSsl = _mailSettings.EnableSsl
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_mailSettings.SenderEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(to);

        await smtpClient.SendMailAsync(mailMessage);
    }
}