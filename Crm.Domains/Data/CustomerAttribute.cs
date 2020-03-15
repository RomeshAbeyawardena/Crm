using Crm.Domains.Constants;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;

namespace Crm.Domains.Data
{
#pragma warning disable CA1819
    public class CustomerAttribute
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public int CustomerId { get; set; }
        [Encrypt(Encryption.PersonalDataKey, EncryptionMethod.Encryption, StringCase.None)]
        public byte[] Value { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        [Modifier(ModifierFlag.Created | ModifierFlag.Modified)]
        public DateTimeOffset Modified { get; set; }

        public virtual Attribute Attribute { get; set; }
    }
    #pragma warning restore CA1819
}
