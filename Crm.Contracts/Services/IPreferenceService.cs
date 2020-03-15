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
    public interface IPreferenceService : IDataService<Domains.Data.Preference>
    {
        Preference GetPreference(IEnumerable<Preference> preferences, string key);
        Task<IEnumerable<Preference>> GetPreferences(CancellationToken cancellationToken);
    }
}
