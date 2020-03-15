using Crm.Contracts.Services;
using Crm.Domains.Data;
using DNI.Core.Contracts;
using DNI.Core.Services.Abstraction;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class CategoryService : DataServiceBase<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> categoryRepository)
            : base(categoryRepository, false)
        {
            
        }

        public async Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken)
        {
            return await Repository.For(DefaultQuery).ToArrayAsync(cancellationToken);
        }
    }
}
