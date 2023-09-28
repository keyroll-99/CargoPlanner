namespace CargoApp.Core.Infrastructure.Mail.Smtp;

public class SmtpOptions
{
    public required string Host { get; init; }
    public int Port { get; init; }
    public bool EnableSsl { get; init; }
    public required string MailFrom { get; init; }
    public required string Password { get; init; }
}