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
            DefaultConnectionString = configuration.GetConnectionString(DataConstants.ConnectionStringKey);
        }
        public string TextEncoding { get; set; }
        public Encoding Encoding => Encoding.GetEncoding(TextEncoding);
        public string DefaultConnectionString { get; set; }
        public IDictionary<string, ConfigCryptographicCredentials> EncryptionKeys { get; set; }
        public long? MemoryCacheSizeLimit { get; set; }
        public double MemoryCacheCompactionPercentage { get; set; }
        public TimeSpan MemoryCacheExpirationScanFrequency { get; set; }
    }
}
