namespace CargoApp.Core.Abstraction.Mail;

public interface IMailManager
{
    Task SendMailAsync(MailModel mailModel);
    Task SendMailAsync<T>(MailModel mailModel, T templateModel);
}