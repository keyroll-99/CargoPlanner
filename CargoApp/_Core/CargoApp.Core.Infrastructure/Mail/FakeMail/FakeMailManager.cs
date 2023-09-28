using CargoApp.Core.Abstraction.Mail;
using Serilog;

namespace CargoApp.Core.Infrastructure.Mail.FakeMail;

public class FakeMailManager : IMailManager
{
    private readonly ILogger _logger;

    public FakeMailManager(ILogger logger)
    {
        _logger = logger;
    }

    public Task SendMailAsync(MailModel mailModel)
    {
        _logger.Information(
            "Send mail to {to}, subject {subject}, mail body {body}", 
            mailModel.To, 
            mailModel.Subject,
            mailModel.Body);
        return Task.CompletedTask;
    }
}