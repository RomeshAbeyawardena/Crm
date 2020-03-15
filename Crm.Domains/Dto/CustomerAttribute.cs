using Crm.Domains.Constants;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;

namespace Crm.Domains.Dto
{
    public class CustomerAttribute
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public int CustomerId { get; set; }

        [Encrypt(Encryption.PersonalDataKey, EncryptionMethod.Encryption, StringCase.None)]
        public string Value { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }

        public Data.Attribute Attribute { get; set; }
    }
}
