using Crm.Domains.Data;
using DNI.Core.Contracts.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface IPreferenceService : IDataService<Preference>
    {
        Preference GetPreference(IEnumerable<Preference> preferences, string key);
        Task<IEnumerable<Preference>> GetPreferences(CancellationToken cancellationToken);
        IEnumerable<Preference> GetPreferencesByCategory(IEnumerable<Preference> preferences, int id);
        Preference GetPreference(IEnumerable<Preference> preferences, int preferenceId);
        Task<Preference> Save(Preference preference, bool saveChanges, bool detachAfterSave, CancellationToken cancellationToken);
    }
}
