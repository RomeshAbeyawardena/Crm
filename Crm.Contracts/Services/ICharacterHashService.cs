using Crm.Domains;
using Crm.Domains.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICharacterHashService
    {
        IEnumerable<char> GetCharacters(string value);
        bool ContainsCharacters(IEnumerable<char> characters, string value);
        IEnumerable<Hash> GetHashes(IHashes hashes, IEnumerable<char> characters);
    }
}
