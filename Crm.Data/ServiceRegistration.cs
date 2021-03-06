﻿using Crm.Domains;
using Crm.Domains.Data;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Options;
using DNI.Core.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Crm.Data
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IServiceRegistrationOptions options)
        {
            services.RegisterDbContextRepositories<CrmDbContext>(configure =>
            {
                configure.UseDbContextPool = true;
                configure.DbContextServiceProviderOptions = ConfigureDbContext;
                configure.EntityTypeDescriber = describer => describer
                    .Describe<Domains.Data.Attribute>()
                    .Describe<Category>()
                    .Describe<Customer>()
                    .Describe<CustomerAttribute>()
                    .Describe<CustomerHash>()
                    .Describe<CustomerPreference>()
                    .Describe<Preference>();
                configure.ServiceLifetime = ServiceLifetime.Transient;
            });
        }

        private void ConfigureDbContext(IServiceProvider services, DbContextOptionsBuilder builder)
        {
            var applicationSettings = services.GetRequiredService<ApplicationSettings>();
            builder.EnableSensitiveDataLogging();
            builder.UseSqlServer(applicationSettings.DefaultConnectionString);
        }
    }
}
