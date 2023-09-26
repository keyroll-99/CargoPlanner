namespace CargoApp.Core.Abstraction.Mail;

public interface IMailManager
{
    Task SendMail(MailModel mailModel);
}