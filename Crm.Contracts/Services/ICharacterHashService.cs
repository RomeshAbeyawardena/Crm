using Crm.Domains.Contracts;
using Crm.Domains.Dto;
using System.Collections.Generic;

namespace Crm.Contracts.Services
{
    public interface ICharacterHashService
    {
        IEnumerable<CharacterIndex> GetCharacters(string value);
        bool ContainsCharacters(IEnumerable<char> characters, string value);
        IEnumerable<CharacterIndex> GetHashes(IHashes hashes, IEnumerable<CharacterIndex> characters);
    }
}
