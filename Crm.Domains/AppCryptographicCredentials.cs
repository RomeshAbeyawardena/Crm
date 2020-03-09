using DNI.Core.Contracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains
{
    public class AppCryptographicCredentials : ICryptographicCredentials
    {
        public IEnumerable<byte> Key { get; set; }
        public IEnumerable<byte> InitialVector { get; set; }
        public string SymmetricAlgorithm { get; set; }
        public KeyDerivationPrf KeyDerivationPrf { get; set; }
        public int Iterations { get; set; }
        public int TotalNumberOfBytes { get; set; }
        public Encoding Encoding { get; set; }
    }
}
