using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DNI.Core.Services.Extensions;
using Crm.Broker;
using Microsoft.Extensions.Caching.Memory;
using Crm.Domains;
using Hangfire;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Threading.Tasks;

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
                .AddDistributedMemoryCache(SetupDistributedCache)
                .RegisterServiceBroker<ServiceBroker>(configure =>
                {
                    configure.RegisterAutoMappingProviders = true;
                    configure.RegisterCacheProviders = true;
                    configure.RegisterCryptographicProviders = true;
                    configure.RegisterExceptionHandlers = true;
                    configure.RegisterMediatorServices = true;
                    configure.RegisterMessagePackSerialisers = true;
                    configure.RegisterJsonSerializerOptions((serviceProvider, options) =>
                    {
                        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    });
                    configure.RegisterJsonFileCacheTrackerStore((serviceProvider, configuration) =>
                    {
                        var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                        configuration.FileName = Path.Combine(webHostEnvironment.ContentRootPath, "cache.json");
                    });
                }, out var serviceBroker);

                services
                    .AddControllersWithViews();

                //.AddApiExplorer();

        }

        private void SetupDistributedCache(MemoryDistributedCacheOptions options)
        {
            options.SizeLimit = _applicationSettings.MemoryCacheSizeLimit;
            options.CompactionPercentage = _applicationSettings.MemoryCacheCompactionPercentage;
            options.ExpirationScanFrequency = _applicationSettings.MemoryCacheExpirationScanFrequency;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IServiceProvider serviceProvider,
            ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            //app.UseResponseCompression();
            
            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
            });

            app.UseStatusCodePages();
            var configureGlobalConfiguration = serviceProvider
                .GetRequiredService<Func<IServiceProvider, IGlobalConfiguration>>();

            configureGlobalConfiguration(serviceProvider);
        }

    }
}
