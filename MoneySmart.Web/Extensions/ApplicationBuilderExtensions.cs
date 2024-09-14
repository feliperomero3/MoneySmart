using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace MoneySmart.Extensions;

[ExcludeFromCodeCoverage]
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseStaticFilesDefaultCache(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var options = new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.GetTypedHeaders().CacheControl =
                    new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(31536000),
                        SharedMaxAge = TimeSpan.FromSeconds(31536000),
                        Extensions =
                        {
                            new NameValueHeaderValue("immutable")
                        }
                    };
            }
        };

        return app.UseMiddleware<StaticFileMiddleware>(Options.Create(options));
    }
}
