using CargoApp.Core.Abstraction.Mail;
using Serilog;

namespace CargoApp.Core.Infrastructure.Mail.FakeMail;

internal class FakeMailManager : IMailManager
{
    private readonly ILogger _logger;
    private readonly IMailProcessor _mailProcessor;
        
    public FakeMailManager(ILogger logger, IMailProcessor mailProcessor)
    {
        _logger = logger;
        _mailProcessor = mailProcessor;
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

    public async Task SendMailAsync<T>(MailModel mailModel, T templateModel)
    {
        var template = await _mailProcessor.Process(templateModel);
        mailModel.Body = template;
        await SendMailAsync(mailModel);
    }
}