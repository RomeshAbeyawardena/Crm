using Crm.Domains.Data;
using DNI.Core.Contracts.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICategoryService : IDataService<Category>
    {
        Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken);
        Category GetCategory(IEnumerable<Category> categories, string categoryName);
        Category GetCategory(IEnumerable<Category> categories, int categoryId);
        Task<Category> Save(Category category, bool saveChanges, bool detachAfterSave, CancellationToken cancellationToken);
    }
}
