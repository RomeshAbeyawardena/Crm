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
    }
}
