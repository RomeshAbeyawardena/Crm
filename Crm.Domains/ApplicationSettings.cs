using Crm.Domains.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DataConstants = Crm.Domains.Constants.Data;

namespace Crm.Domains
{
    public class ApplicationSettings
    {
        public ApplicationSettings(IConfiguration configuration)
        {
            configuration.Bind(this);
            DefaultConnectionString = configuration.GetConnectionString(DataConstants.DefaultConnectionStringKey);
            HangfireConnectionString = configuration.GetConnectionString(DataConstants.HangfireConnectionStringKey);
        }
        public string TextEncoding { get; set; }
        public Encoding Encoding => Encoding.GetEncoding(TextEncoding);
        public string DefaultConnectionString { get; set; }
        public IDictionary<string, ConfigCryptographicCredentials> EncryptionKeys { get; set; }
        public IDictionary<string, string> Hashes { get; set; }
        public IHashes GetHashes() => Hash.CreateHashes(Hashes);
        public long? MemoryCacheSizeLimit { get; set; }
        public double MemoryCacheCompactionPercentage { get; set; }
        public TimeSpan MemoryCacheExpirationScanFrequency { get; set; }
        public string HangfireConnectionString { get; set; }
    }
}
