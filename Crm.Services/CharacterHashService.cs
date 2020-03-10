using Crm.Contracts.Services;
using Crm.Domains;
using Crm.Domains.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class CharacterHashService : ICharacterHashService
    {
        public bool ContainsCharacters(IEnumerable<char> characters, string value)
        {
            return value.ToLower().All(v => characters.Contains(v));
        }

        public IEnumerable<char> GetCharacters(string value)
        {
            return value.ToLower().ToCharArray().OrderBy(c => c);
        }

        public IEnumerable<Hash> GetHashes(IHashes hashes, IEnumerable<char> characters)
        {
            foreach(var character in characters)
                yield return hashes.GetHash(character);
        }
    }
}
