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

    }
}
