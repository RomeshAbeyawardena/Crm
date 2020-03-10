using Hangfire;
using Hangfire.Server;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Services
{
    internal sealed class NetCoreJobActivatorScope : JobActivatorScope
    {
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope _serviceScope;

        public NetCoreJobActivatorScope(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object Resolve(Type type)
        {
            return (_serviceScope = _serviceProvider.CreateScope())
                .ServiceProvider
                .GetRequiredService(type);
        }

        public override void DisposeScope()
        {
            _serviceScope?.Dispose();
        }
    }

    public sealed class NetCoreJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;
        
        public NetCoreJobActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            return new NetCoreJobActivatorScope(_serviceProvider);
        }

        public override JobActivatorScope BeginScope(PerformContext context)
        {
            return new NetCoreJobActivatorScope(_serviceProvider);
        }
        
        public override object ActivateJob(Type jobType)
        {
            return _serviceProvider.GetService(jobType);
        }
    }
}
