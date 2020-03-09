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
using Crm.Domains.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace Crm.Services
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IServiceRegistrationOptions options)
        {
            services
                .AddSingleton<ApplicationSettings>()
                .AddAutoMapper(Assembly.GetAssembly(typeof(DomainProfile)))
                .RegisterCryptographicCredentialsFactory<AppCryptographicCredentials>(ConfigureCryptographicCredentialsFactory)
                .AddMediatR(Assembly.GetAssembly(typeof(ServiceRegistration)))
                .Scan(scan => scan
                .FromAssemblyOf<ServiceRegistration>()
                .AddClasses()
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }

        private void ConfigureCryptographicCredentialsFactory(ISwitch<string, ICryptographicCredentials> credentialsSwitch, ICryptographyProvider cryptographicProvider, IServiceProvider serviceProvider)
        {
            var applicationSettings = serviceProvider.GetRequiredService<ApplicationSettings>();
            var mapperProvider = serviceProvider.GetRequiredService<IMapperProvider>();

            if (applicationSettings.EncryptionKeys.TryGetValue(Encryption.IdentificationKey, out var identificationCredentials))
                credentialsSwitch.CaseWhen(Encryption.IdentificationKey, mapperProvider
                    .Map<ConfigCryptographicCredentials, AppCryptographicCredentials>(identificationCredentials));

            if(applicationSettings.EncryptionKeys.TryGetValue(Encryption.PersonalDataKey, out var personalDataCredentials))
                credentialsSwitch.CaseWhen(Encryption.IdentificationKey, mapperProvider
                    .Map<ConfigCryptographicCredentials, AppCryptographicCredentials>(personalDataCredentials));
        }
    }
}
