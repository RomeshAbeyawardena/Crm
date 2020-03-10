using Crm.Domains.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains
{
    /// <summary>
    /// Represents a hash
    /// </summary>
    public class Hash
    {
        public static IHashes CreateHashes(IDictionary<string, string> hashDictionary)
        {
            return Hashes.Create(hashDictionary);
        }
        public char Character { get; set; }
        public string HashValue { get; set; }
        public IEnumerable<byte> Value { get; set; }
    }

    /// <summary>
    /// Represents a dictionary of hashes
    /// </summary>
    internal class Hashes : IHashes
    {
        private readonly IDictionary<char, IEnumerable<byte>> _hashDictionary;
        private readonly IDictionary<char, string> _originalHashDictionary;

        private Hashes(IDictionary<string, string> hashDictionary)
        {
            _originalHashDictionary = hashDictionary
                .ToDictionary(key => key.Key.FirstOrDefault(), value => value.Value);
            _hashDictionary = new Dictionary<char, IEnumerable<byte>>();

            foreach(var (key, value) in _originalHashDictionary)
                _hashDictionary.Add(key, Convert.FromBase64String(value));

        }

        public static IHashes Create(IDictionary<string, string> hashDictionary)
        {
            return new Hashes(hashDictionary);
        }

        public Hash GetHash(char character)
        {
            var hashValue = GetHashValue(character);
            if(hashValue == null || !_originalHashDictionary.TryGetValue(character, out var hashedValue))
                return default;

            return new Hash { Character = character, Value = hashValue, HashValue = hashedValue};
        }

        public IEnumerable<byte> GetHashValue(char character)
        {
            if(_hashDictionary.TryGetValue(character, out var value))
                return value;

            return default;
        }

    }
}
