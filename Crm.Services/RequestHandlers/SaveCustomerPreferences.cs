using Crm.Contracts.Providers;
using Crm.Contracts.Services;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.RequestHandlers
{
    public class SaveCustomerPreferences : RequestHandlerBase<SaveCustomerPreferencesRequest, SaveCustomerPreferencesResponse>
    {
        private readonly ICustomerPreferenceService _customerPreferenceService;
        private readonly ICrmCacheProvider _cacheProvider;
        private readonly IPreferenceService _preferenceService;

        public SaveCustomerPreferences(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider, 
            ICustomerPreferenceService customerPreferenceService,
            IPreferenceService preferenceService, ICrmCacheProvider cacheProvider) 
            : base(mapperProvider, encryptionProvider)
        {
            _customerPreferenceService = customerPreferenceService;
            _cacheProvider = cacheProvider;
            _preferenceService = preferenceService;
        }

        public override async Task<SaveCustomerPreferencesResponse> Handle(SaveCustomerPreferencesRequest request, CancellationToken cancellationToken)
        {
            var optedInPreferences = request.CustomerPreferences
                .Where(customerPreference => customerPreference.Value == true);
            var optedOutPreferences = request.CustomerPreferences
                .Where(customerPreference => customerPreference.Value == false);
            var invalidPreferences = new List<string>();
            var preferences = await _cacheProvider.GetPreferences(cancellationToken);
            var customerPreferences = await _customerPreferenceService
                .GetCustomerPreferences(request.CustomerId, request.FromDate, cancellationToken);

            var savedCustomerPreferences = new List<CustomerPreference>();

            foreach (var preference in request.CustomerPreferences)
            {
                Preference foundPreference;

                if((foundPreference = _preferenceService.GetPreference(preferences, preference.Key)) == null)
                {
                    invalidPreferences.Add(preference.Key);
                    continue;
                }

                var customerPreference = _customerPreferenceService.GetCustomerPreference(customerPreferences, foundPreference.Id);

                if(customerPreference == null)
                    customerPreference = new CustomerPreference
                    {
                        CustomerId = request.CustomerId,
                        PreferenceId = foundPreference.Id
                    };

                customerPreference.OptInDate = request.FromDate;
                customerPreference.NextCheckInDate = request.NextCheckInDate;

                savedCustomerPreferences.Add(await _customerPreferenceService
                    .Save(customerPreference, false, cancellationToken));
            }

            if(savedCustomerPreferences.Count > 0)
                await _customerPreferenceService
                    .CommitChanges(cancellationToken);

            return Response.Success<SaveCustomerPreferencesResponse>(savedCustomerPreferences.ToArray(), model => { 
                model.InalidPreferences = invalidPreferences.ToArray();
                model.TotalOptedIn = optedInPreferences.Count();
                model.TotalOptedOut = optedOutPreferences.Count();
            });
        }
    }
}
