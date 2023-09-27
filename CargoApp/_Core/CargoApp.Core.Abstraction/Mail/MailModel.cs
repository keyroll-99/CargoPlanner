namespace CargoApp.Core.Abstraction.Mail;

public record MailModel
{
    public string Body { get; private set; }
    public string To { get; private set; }
    public string Subject { get; private set; }

    private MailModel(string body, string to, string subject)
    {
        Body = body;
        To = to;
        Subject = subject;
    }
    
    public static MailModel CreateModel(string body, string to, string subject)
    {
        if (string.IsNullOrWhiteSpace(body) || string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(subject))
        {
            throw new ArgumentException("Invalid argument for mail model");
        }

        return new MailModel(body, to, subject);
    }
};