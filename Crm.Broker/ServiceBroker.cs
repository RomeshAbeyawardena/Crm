using DNI.Core.Services.Abstraction;
using Crm.Services;
using System;
using DataServiceRegistration = Crm.Data.ServiceRegistration;
using Microsoft.Extensions.DependencyInjection;
using Crm.Domains;
using Hangfire;

namespace Crm.Broker
{
    public class ServiceBroker : ServiceBrokerBase
    {
        public ServiceBroker()
        {
            DescribeAssemblies = describer => describer
                .GetAssembly<ServiceRegistration>()
                .GetAssembly<DataServiceRegistration>();
        }

        public static void ConfigureHangfire(IServiceProvider serviceProvider, IGlobalConfiguration configuration)
        {
            var applicationSettings = serviceProvider.GetService<ApplicationSettings>();
            var netCoreJobActivator = serviceProvider.GetService<NetCoreJobActivator>();
            configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseActivator(netCoreJobActivator)
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(applicationSettings.HangfireConnectionString);
        }
    }
}
