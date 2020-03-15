using Crm.Contracts.Services;
using DNI.Core.Contracts;
using DNI.Core.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class AttributeService : DataServiceBase<Domains.Data.Attribute>, IAttributeService
    {
        public async Task<Domains.Data.Attribute> SaveAttribute(Domains.Data.Attribute attribute, bool v, CancellationToken cancellationToken)
        {
            return await Repository.SaveChanges(attribute, v, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Domains.Data.Attribute>> GetAttributes(CancellationToken cancellationToken)
        {
            var query = from attribute in DefaultQuery
                        select attribute;

            return await Repository
                .For(query)
                .ToArrayAsync(cancellationToken);
        }

        public Domains.Data.Attribute GetAttribute(IEnumerable<Domains.Data.Attribute> attributes, string attributeKey)
        {
            return attributes.SingleOrDefault(attribute => attribute.Key.Equals(attributeKey, StringComparison.InvariantCultureIgnoreCase ));
        }

        public Domains.Data.Attribute GetAttribute(IEnumerable<Domains.Data.Attribute> attributes, int id)
        {
            return attributes.SingleOrDefault(attribute => attribute.Id == id);
        }

        public AttributeService(IRepository<Domains.Data.Attribute> attributeRepository)
            : base(attributeRepository, false)
        {
            
        }
    }
}
