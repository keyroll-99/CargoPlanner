using CargoApp.Core.Abstraction.Mail;
using CargoApp.Core.Infrastructure.Mail.FakeMail;
using CargoApp.Core.Infrastructure.Mail.Smtp;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Core.Infrastructure.Mail;

internal static class Extensions
{
    private const string MailOptionsSectionName = "Mail";

    public static IServiceCollection AddMail(this IServiceCollection services)
    {
        var mailOptions = services.GetOptions<MailOptions>(MailOptionsSectionName);
        services.AddSingleton(mailOptions);

        switch (mailOptions.ServerType)
        {
            case MailServerTypeEnum.Fake:
                services.AddSingleton<IMailManager, FakeMailManager>();
                break;
            case MailServerTypeEnum.Smtp:
                services.AddSingleton<IMailManager, SmtpMailManager>();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        services.AddSingleton<IMailProcessor, MailProcessor>();

        return services;
    }
}