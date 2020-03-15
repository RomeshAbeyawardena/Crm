using DNI.Core.Services.Abstraction;
using Crm.Services;
using DataServiceRegistration = Crm.Data.ServiceRegistration;

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
