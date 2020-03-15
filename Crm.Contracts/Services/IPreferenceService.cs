using Crm.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface IPreferenceService
    {
        Preference GetPreference(IEnumerable<Preference> preferences, string key);
    }
}
