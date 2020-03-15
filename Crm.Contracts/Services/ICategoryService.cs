using Crm.Domains.Data;
using DNI.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICategoryService : IDataService<Domains.Data.Category>
    {
        Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken);
    }
}
