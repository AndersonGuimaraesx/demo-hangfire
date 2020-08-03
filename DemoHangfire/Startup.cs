using DemoHangfire.Classes;
using DemoHangfire.Interfaces;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace DemoHangfire
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHangfire(config =>
                            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                            .UseSimpleAssemblyNameTypeSerializer()
                            .UseDefaultTypeSerializer()
                            .UseMemoryStorage());

            //services.AddHangfire(config =>
            //   config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //   .UseSimpleAssemblyNameTypeSerializer()
            //   .UseDefaultTypeSerializer()
            //   .UseSqlServerStorage(Configuration["Hangfire"]));

            services.AddHangfireServer();

            services.AddSingleton<IPrintJob, PrintJob>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseHangfireDashboard();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] { new NoAuthFilter() },
            });

            backgroundJobClient.Enqueue(() => Console.WriteLine("Hello Hangfire job!"));
            recurringJobManager.AddOrUpdate(
               "Run every minute",
               () => serviceProvider.GetService<IPrintJob>().Print(),
               "* * * * *"
               );
        }
    }
}
