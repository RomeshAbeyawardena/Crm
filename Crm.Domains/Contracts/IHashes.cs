using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Contracts
{
    /// <summary>
    /// Represents a dictionary of character hashes
    /// </summary>
    public interface IHashes
    {
        Hash GetHash(char character);
        IEnumerable<byte> GetHashValue(char character);
    }
}
