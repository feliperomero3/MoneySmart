using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Extensions.DependencyInjection;
using MoneySmart.Services;

namespace MoneySmart.Telemetry;

public static class ApplicationTelemetryServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationTelemetry(this IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetry();
        services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
        {
            module.EnableSqlCommandTextInstrumentation = true;
        });

        services.AddSingleton<ITelemetryService, ApplicationTelemetry>()
                .AddSingleton<UserTelemetryMiddleware>();

        return services;
    }
}
