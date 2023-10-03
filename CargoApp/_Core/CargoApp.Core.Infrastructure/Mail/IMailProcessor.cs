using System.Reflection;

namespace CargoApp.Core.Infrastructure.Mail;

internal interface IMailProcessor
{
    public Task<string> Process<T>(T model);
}