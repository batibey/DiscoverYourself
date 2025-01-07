namespace DiscoverYourself.Models.MailModels;

public class MailSettings
{
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string SenderEmail { get; set; }
    public string SenderPassword { get; set; }
    public bool EnableSsl { get; set; }
}