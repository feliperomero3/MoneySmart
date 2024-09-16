using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneySmart.Data;
using MoneySmart.Extensions;
using MoneySmart.Telemetry;
using MoneySmart.Services;

namespace MoneySmart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(o =>
            {
                o.LoginPath = new PathString("/Identity/Account/Login");
                o.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
                };
            });

            services.AddControllers();
            services.AddRazorPages();

            services.AddAuthorization(config =>
            {
                config.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
            services.AddApplicationInsightsTelemetry();
            services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
            {
                module.EnableSqlCommandTextInstrumentation = true;
            });

            services.AddSingleton<ITelemetryService, ApplicationTelemetry>()
                    .AddSingleton<UserTelemetryMiddleware>()
                    .AddSingleton<IEmailSender, EmailSender>();

            services.AddResponseCaching();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                app.UseResponseCaching();
            }

            app.UseHttpsRedirection();

            app.UseStaticFilesDefaultCache();

            app.UseRouting();

            app.UseHealthChecks("/health");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseUserTelemetry();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
