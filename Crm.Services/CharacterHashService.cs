using Crm.Contracts.Services;
using Crm.Domains.Contracts;
using Crm.Domains.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crm.Services
{
    public class CharacterHashService : ICharacterHashService
    {
        public bool ContainsCharacters(IEnumerable<char> characters, string value)
        {
            return value.ToUpper().All(v => characters.Contains(v));
        }

        public IEnumerable<CharacterIndex> GetCharacters(string value)
        {
            var currentCharacterIndex = 0;
            foreach(var character in value.ToUpper())
                yield return new CharacterIndex { Character = character, Index = currentCharacterIndex++ };
        }

        public IEnumerable<CharacterIndex> GetHashes(IHashes hashes, IEnumerable<CharacterIndex> characters)
        {
            foreach(var character in characters){

                character.Hash = hashes.GetHash(character.Character);
                yield return character;
            }
        }
    }
}
