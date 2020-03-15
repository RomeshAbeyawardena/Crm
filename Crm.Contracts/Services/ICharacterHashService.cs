using Crm.Domains;
using Crm.Domains.Contracts;
using Crm.Domains.Dto;
using DNI.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface ICharacterHashService
    {
        IEnumerable<CharacterIndex> GetCharacters(string value);
        bool ContainsCharacters(IEnumerable<char> characters, string value);
        IEnumerable<CharacterIndex> GetHashes(IHashes hashes, IEnumerable<CharacterIndex> characters);
    }
}
