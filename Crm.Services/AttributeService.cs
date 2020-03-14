using Crm.Contracts.Services;
using DNI.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IRepository<Domains.Data.Attribute> _attributeRepository;

        private IQueryable<Domains.Data.Attribute> DefaultAttributeQuery => _attributeRepository.Query();
        
        public async Task<Domains.Data.Attribute> SaveAttribute(Domains.Data.Attribute attribute, bool v, CancellationToken cancellationToken)
        {
            return await _attributeRepository.SaveChanges(attribute, v, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Domains.Data.Attribute>> GetAttributes(CancellationToken cancellationToken)
        {
            var query = from attribute in DefaultAttributeQuery
                        select attribute;

            return await _attributeRepository
                .For(query)
                .ToArrayAsync(cancellationToken);
        }

        public Domains.Data.Attribute GetAttribute(IEnumerable<Domains.Data.Attribute> attributes, string attributeKey)
        {
            return attributes.SingleOrDefault(attribute => attribute.Key.Equals(attributeKey, StringComparison.InvariantCultureIgnoreCase ));
        }

        public AttributeService(IRepository<Domains.Data.Attribute> attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }
    }
}
