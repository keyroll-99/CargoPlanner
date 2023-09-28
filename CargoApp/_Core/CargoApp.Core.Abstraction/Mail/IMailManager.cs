namespace CargoApp.Core.Abstraction.Mail;

public interface IMailManager
{
    Task SendMailAsync(MailModel mailModel);
}