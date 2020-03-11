﻿using DNI.Core.Contracts;
using DNI.Core.Contracts.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using MediatR;
using System.Reflection;
using AutoMapper;
using Crm.Domains;
using DNI.Core.Services.Extensions;
using DNI.Core.Contracts.Providers;
using Crm.Domains.Constants;
using Microsoft.Extensions.Caching.Memory;
using Hangfire;

namespace Crm.Services
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IServiceRegistrationOptions options)
        {
            services
                .AddSingleton(ConfigureHangfire)
                .AddSingleton<NetCoreJobActivator>()
                .AddSingleton<NetCoreJobActivatorScope>()
                .AddSingleton<ApplicationSettings>()
                .AddAutoMapper(ConfigureAutoMapper, Assembly.GetAssembly(typeof(DomainProfile)))
                .RegisterCryptographicCredentialsFactory<AppCryptographicCredentials>(ConfigureCryptographicCredentialsFactory)
                .AddMediatR(Assembly.GetAssembly(typeof(ServiceRegistration)))
                .Scan(scan => scan
                    .FromAssemblyOf<ServiceRegistration>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            
        }

        private Func<IServiceProvider, IGlobalConfiguration> ConfigureHangfire(IServiceProvider serviceProvider)
        {
            return (serviceProvider) =>
            {
                var applicationSettings = serviceProvider.GetService<ApplicationSettings>();
                var netCoreJobActivator = serviceProvider.GetService<NetCoreJobActivator>();

                return GlobalConfiguration.Configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseActivator(netCoreJobActivator)
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(applicationSettings.HangfireConnectionString);
            };
        }

        private void ConfigureAutoMapper(IServiceProvider serviceProvider, IMapperConfigurationExpression configuration)
        {
            configuration.ConstructServicesUsing(serviceProvider.GetService);
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
