using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataConstants = Crm.Domains.Constants.Data;

namespace Crm.Domains
{
    public class ApplicationSettings
    {
        public ApplicationSettings(IConfiguration configuration)
        {
            configuration.Bind(this);
            configuration.GetConnectionString(DataConstants.ConnectionStringKey);
        }

        public string DefaultConnectionString { get; set; }
        public IDictionary<string, AppCryptographicCredentials> EncryptionKeys { get; set; }
    }
}
