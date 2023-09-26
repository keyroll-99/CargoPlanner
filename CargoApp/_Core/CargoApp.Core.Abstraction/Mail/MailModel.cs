namespace CargoApp.Core.Abstraction.Mail;

public record MailModel
{
    public string Body { get; private set; }
    public string To { get; private set; }

    private MailModel(string body, string to)
    {
        Body = body;
        To = to;
    }
    
    public static MailModel CreateModel(string body, string to)
    {
        if (string.IsNullOrWhiteSpace(body) || string.IsNullOrWhiteSpace(to))
        {
            throw new ArgumentException("Invalid argument for mail model");
        }

        return new MailModel(body, to);
    }
};