using CargoApp.Core.Abstraction.Mail;

namespace CargoApp.Core.Infrastructure.Mail.Smtp;

public class SmtpMailManager : IMailManager
{
    private readonly SmtpOptions _smtpOptions;

    public SmtpMailManager(MailOptions mailOptions)
    {
        ArgumentNullException.ThrowIfNull(mailOptions?.SmtpOptions);
        _smtpOptions = mailOptions.SmtpOptions;
    }

    
    public Task SendMail(MailModel mailModel)
    {
        throw new NotImplementedException();
    }
}