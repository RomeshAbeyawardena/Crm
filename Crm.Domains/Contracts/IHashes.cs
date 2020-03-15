using System.Collections.Generic;

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
