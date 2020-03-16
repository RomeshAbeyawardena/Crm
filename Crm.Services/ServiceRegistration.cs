using DNI.Core.Contracts;
using DNI.Core.Contracts.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using MediatR;
using System.Reflection;
using AutoMapper;
using Crm.Domains;
using DNI.Core.Services.Extensions;
using DNI.Core.Contracts.Providers;
using Hangfire;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Crm.Services
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IServiceRegistrationOptions options)
        {
                services
                .AddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>()
                .TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, DefaultApiDescriptionProvider>());
                
                services.AddTransient<IApiDescriptionProvider, DefaultApiDescriptionProvider>()
                .AddSingleton(ConfigureHangfire)
                .AddSingleton<ApplicationSettings>()
                .AddAutoMapper(ConfigureAutoMapper, Assembly.GetAssembly(typeof(DomainProfile)))
                .RegisterCryptographicCredentialsFactory<AppCryptographicCredentials>(ConfigureCryptographicCredentialsFactory)
                .AddMediatR(Assembly.GetAssembly(typeof(ServiceRegistration)))
                .Scan(scan => scan
                    .FromAssemblyOf<ServiceRegistration>()
                    .AddClasses(implementation => implementation.Where(
                        imp => imp.Name.EndsWith("Service") || imp.Name.EndsWith("Provider") || imp.Name.EndsWith("Activator")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            
        }

        private Func<IServiceProvider, IGlobalConfiguration> ConfigureHangfire(IServiceProvider serviceProvider)
        {
            return (serviceProvider) =>
            {
                var applicationSettings = serviceProvider.GetService<ApplicationSettings>();

                return GlobalConfiguration.Configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseDefaultDependencyInjectionActivator(serviceProvider)
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(applicationSettings.HangfireConnectionString);
            };
        }

        private void ConfigureAutoMapper(IServiceProvider serviceProvider, IMapperConfigurationExpression configuration)
        {
            configuration
                .ConstructServicesUsing(serviceProvider.GetService);
            
        }

        private void ConfigureCryptographicCredentialsFactory(ISwitch<string, ICryptographicCredentials> credentialsSwitch, ICryptographyProvider cryptographicProvider, IServiceProvider serviceProvider)
        {
            var applicationSettings = serviceProvider.GetRequiredService<ApplicationSettings>();
            var mapperProvider = serviceProvider.GetRequiredService<IMapperProvider>();

            foreach(var keyValue in applicationSettings.EncryptionKeys)
                credentialsSwitch.CaseWhen(keyValue.Key, mapperProvider
                    .Map<ConfigCryptographicCredentials, AppCryptographicCredentials>(keyValue.Value));
            
        }
    }
}
