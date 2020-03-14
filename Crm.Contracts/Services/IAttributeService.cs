using Newtonsoft.Json.Linq;
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
        Task<Domains.Data.Attribute> SaveAttribute(Domains.Data.Attribute attribute, bool v, CancellationToken cancellationToken);
        Task<IEnumerable<Domains.Data.Attribute>> GetAttributes(CancellationToken cancellationToken);
        Domains.Data.Attribute GetAttribute(IEnumerable<Domains.Data.Attribute> attributes, string attributeKey);
    }
}
