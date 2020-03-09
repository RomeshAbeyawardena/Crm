using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Crm.Domains
{
    public class ConfigCryptographicCredentials
    {
        public string Key { get; set; }
        public string InitialVector { get; set; }
        public string SymmetricAlgorithm { get; set; }
        public KeyDerivationPrf KeyDerivationPrf { get; set; }
        public int Iterations { get; set; }
        public int TotalNumberOfBytes { get; set; }
        public string Encoding { get; set; }
    }
}
