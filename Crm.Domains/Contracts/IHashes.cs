using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Contracts
{
    public interface IHashes
    {
        Hash GetHash(char character);
        IEnumerable<byte> GetHashValue(char character);
    }
}
