using Crm.Contracts.Services;
using Crm.Domains.Data;
using DNI.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IRepository<Preference> _preferenceRepository;
        private IQueryable<Preference> DefaultQuery => _preferenceRepository.Query();
        public Preference GetPreference(IEnumerable<Preference> preferences, string key)
        {
            return preferences.SingleOrDefault(preference => preference.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
        }

        public Task<IEnumerable<Preference>> GetPreferences(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public PreferenceService(IRepository<Preference> preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }
    }
}
