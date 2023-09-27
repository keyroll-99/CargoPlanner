using CargoApp.Core.Infrastructure.Mail.Smtp;

namespace CargoApp.Core.Infrastructure.Mail;

public class MailOptions
{
    public MailServerTypeEnum ServerType { get; init; }
    public SmtpOptions? SmtpOptions { get; init; }
}