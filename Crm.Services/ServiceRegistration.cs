using DNI.Core.Contracts;
using DNI.Core.Contracts.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using MediatR;
using System.Reflection;
using AutoMapper;
using Crm.Domains;

namespace Crm.Services
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IServiceRegistrationOptions options)
        {
            services
                .AddAutoMapper(Assembly.GetAssembly(typeof(DomainProfile)))
                .AddMediatR(Assembly.GetAssembly(typeof(ServiceRegistration)))
                .Scan(scan => scan
                .FromAssemblyOf<ServiceRegistration>()
                .AddClasses()
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );
        }
    }
}
