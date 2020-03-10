using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DNI.Core.Services.Extensions;
using Crm.Broker;
using Microsoft.Extensions.Caching.Memory;
using Crm.Domains;
using Hangfire;
using Hangfire.SqlServer;
using Crm.Services;

namespace Crm.Web
{
    public class Startup
    {
        private ApplicationSettings _applicationSettings;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDistributedMemoryCache(SetupDistrubutedCache)
                .RegisterServiceBroker<ServiceBroker>(configure => { 
                    configure.RegisterAutoMappingProviders = true;
                    configure.RegisterCacheProviders = true; 
                    configure.RegisterCryptographicProviders = true;
                    configure.RegisterExceptionHandlers = true;
                    configure.RegisterMediatorServices = true;
                    configure.RegisterMessagePackSerialisers = true;
                }, out var serviceBroker)
                .AddControllers();
            
            services
                .AddHangfire(ConfigureHangfire)
                .AddHangfireServer(ConfigureHangfireServer);
        }

        private void ConfigureHangfire(IServiceProvider serviceProvider, IGlobalConfiguration configuration)
        {
            var applicationSettings = serviceProvider.GetService<ApplicationSettings>();
            var netCoreJobActivator = serviceProvider.GetService<NetCoreJobActivator>();
            configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseActivator<NetCoreJobActivator>(netCoreJobActivator)
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(applicationSettings.HangfireConnectionString);
        }

        private void ConfigureHangfireServer(BackgroundJobServerOptions serverOptions)
        {
           serverOptions.WorkerCount = 3;
        }

        private void SetupDistrubutedCache(MemoryDistributedCacheOptions options)
        {
            options.SizeLimit = _applicationSettings.MemoryCacheSizeLimit;
            options.CompactionPercentage = _applicationSettings.MemoryCacheCompactionPercentage;
            options.ExpirationScanFrequency = _applicationSettings.MemoryCacheExpirationScanFrequency;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHangfireDashboard();
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
