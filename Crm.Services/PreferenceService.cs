﻿using Crm.Contracts.Services;
using Crm.Domains.Data;
using DNI.Core.Contracts;
using DNI.Core.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class PreferenceService : DataServiceBase<Preference>, IPreferenceService
    {
        public Preference GetPreference(IEnumerable<Preference> preferences, string key)
        {
            return preferences.SingleOrDefault(preference => preference.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<IEnumerable<Preference>> GetPreferences(CancellationToken cancellationToken)
        {
            return await Repository.For(DefaultQuery).ToArrayAsync(cancellationToken);
        }

        public IEnumerable<Preference> GetPreferencesByCategory(IEnumerable<Preference> preferences, int categoryId)
        {
            return preferences.Where(preference => preference.CategoryId == categoryId);
        }

        public Preference GetPreference(IEnumerable<Preference> preferences, int preferenceId)
        {
            return preferences.SingleOrDefault(preference => preference.Id == preferenceId);
        }

        public async Task<Preference> Save(Preference preference, bool saveChanges, bool detachAfterSave, CancellationToken cancellationToken)
        {
            return await Repository.SaveChanges(preference, saveChanges, detachAfterSave, cancellationToken);
        }

        public PreferenceService(IRepository<Preference> preferenceRepository)
            : base(preferenceRepository, false)
        {
            
        }
    }
}
