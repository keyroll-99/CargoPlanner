using CargoApp.Core.Abstraction.Mail;
using Serilog;

namespace CargoApp.Core.Infrastructure.Mail;

public class FakeMailManager : IMailManager
{
    private readonly ILogger _logger;

    public FakeMailManager(ILogger logger)
    {
        _logger = logger;
    }

    public Task SendMail(MailModel mailModel)
    {
        _logger.Information("Send mail to {to}, mail body {body}", mailModel.To, mailModel.Body);
        return Task.CompletedTask;
    }
}