using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneySmart.Authentication;
using MoneySmart.Data;
using MoneySmart.Extensions;
using MoneySmart.Services;
using MoneySmart.Telemetry;

namespace MoneySmart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddApplicationAuthentication();
            services.AddControllers();
            services.AddRazorPages();
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
            services.AddApplicationTelemetry();
            services.AddSingleton<IEmailSender, EmailSender>();
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
            }

            app.UseHttpsRedirection();

            app.UseStaticFilesDefaultCache();

            app.UseResponseCaching();
            app.UseRouting();

            app.UseHealthChecks("/health");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseUserTelemetry();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
