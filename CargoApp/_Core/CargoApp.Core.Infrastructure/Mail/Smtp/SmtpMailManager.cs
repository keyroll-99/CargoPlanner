using System.Net;
using System.Net.Mail;
using CargoApp.Core.Abstraction.Mail;

namespace CargoApp.Core.Infrastructure.Mail.Smtp;

internal class SmtpMailManager : IMailManager
{
    private readonly SmtpOptions _smtpOptions;
    private readonly SmtpClient _smtpClient;
    private readonly IMailProcessor _mailProcessor;
    
    public SmtpMailManager(MailOptions mailOptions, IMailProcessor mailProcessor)
    {
        _mailProcessor = mailProcessor;
        ArgumentNullException.ThrowIfNull(mailOptions?.SmtpOptions);
        _smtpOptions = mailOptions.SmtpOptions!;

        _smtpClient = new SmtpClient(_smtpOptions!.Host, _smtpOptions.Port)
        {
            Credentials = new NetworkCredential(_smtpOptions.MailFrom, _smtpOptions.Password),
            UseDefaultCredentials = false,
            EnableSsl = _smtpOptions.EnableSsl
        };

    }

    
    public Task SendMailAsync(MailModel mailModel)
    {
        var mailMessage = new MailMessage(new MailAddress(_smtpOptions.MailFrom), new MailAddress(mailModel.To));
        mailMessage.Subject = mailModel.Subject;
        mailMessage.Body = mailModel.Body;
        mailMessage.IsBodyHtml = true;
        
        return _smtpClient.SendMailAsync(mailMessage);
    }

    public async Task SendMailAsync<T>(MailModel mailModel, T templateModel)
    {
        var template = await _mailProcessor.Process(templateModel);
        mailModel.Body = template;
        await SendMailAsync(mailModel);
    }
}