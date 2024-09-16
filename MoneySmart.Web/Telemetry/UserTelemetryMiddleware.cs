using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MoneySmart.Telemetry;

/// <summary>
/// Add an Applications Insights 'application-defined' property for the signed-in user for each web request.
/// </summary>
/// <remarks>
/// Defines a User property which will be used to store the user's identity in Applications Insights.
/// If the user is authenticated, it sets the value to the user's name; otherwise, it sets the value to Anonymous.
/// </remarks>
public class UserTelemetryMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        const string key = "User";

        var user = context.User;
        var value = user.Identity is { IsAuthenticated: true }
            ? user.Identity.Name
            : "Anonymous";

        var requestTelemetry = context.Features.Get<RequestTelemetry>();

        requestTelemetry?.Properties.Add(key, value);

        await next(context);
    }
}

public static class UserTelemetryMiddlewareExtensions
{
    public static IApplicationBuilder UseUserTelemetry(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserTelemetryMiddleware>();
    }
}