using Crm.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken);
    }
}
