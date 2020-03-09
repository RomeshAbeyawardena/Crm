using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface IAttributeService
    {
        Task<Domains.Data.Attribute> GetAttribute(string property, CancellationToken cancellationToken);
        Task<Domains.Data.Attribute> SaveAttribute(Domains.Data.Attribute attribute, bool v, CancellationToken cancellationToken);
    }
}
