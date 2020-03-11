using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Broker;
using Crm.Contracts;
using Crm.Domains;
using Crm.Services;
using DNI.Core.Services.Extensions;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Crm.Hangfire.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .RegisterServiceBroker<ServiceBroker>(configure => { 
                    configure.RegisterAutoMappingProviders = true;
                    configure.RegisterCryptographicProviders = true;
                    configure.RegisterExceptionHandlers = true;
                    configure.RegisterMediatorServices = true;
                    configure.RegisterMessagePackSerialisers = true;
                }, out var serviceBroker)
                .AddControllers();
            
            services
                .AddHangfire((serviceProvider, configuration) => serviceProvider
                    .GetRequiredService<Func<IServiceProvider, IGlobalConfiguration>>()
                        ?.Invoke(serviceProvider))

                .AddHangfireServer(ConfigureHangfireServer);
        }


        private void ConfigureHangfireServer(BackgroundJobServerOptions serverOptions)
        {
           serverOptions.WorkerCount = 1;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseHangfireDashboard();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
