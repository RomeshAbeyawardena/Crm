using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Providers
{
    public interface ICrmCacheProvider
    {
        Task<IEnumerable<Domains.Data.Attribute>> GetAttributes(CancellationToken cancellationToken);
    }
}
