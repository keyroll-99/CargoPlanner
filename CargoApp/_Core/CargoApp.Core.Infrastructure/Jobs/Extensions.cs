using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CargoApp.Core.Infrastructure.Jobs;

public static class Extensions
{
    public static IServiceCollection AddJob<TJobs>(this IServiceCollection services, string cronExpression)
        where TJobs : IJob
    {
        var jobKey = typeof(TJobs).FullName;
        services.AddQuartz(sp =>
        {
            sp.AddJob<TJobs>(opt => opt.WithIdentity(jobKey));
            sp.AddTrigger(opt => opt.ForJob(jobKey).WithIdentity($"{jobKey}-trigger").WithCronSchedule(cronExpression));
        });

        return services;
    }
}