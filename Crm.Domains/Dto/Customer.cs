using AutoMapper.Configuration.Annotations;
using Crm.Domains.Constants;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Dto
{
    public class Customer
    {
        public int Id { get; set; }

        [Encrypt(Encryption.IdentificationKey, EncryptionMethod.Encryption, StringCase.Upper)]
        public string EmailAddress { get; set; }

        [Encrypt(Encryption.CommonDataKey, EncryptionMethod.Encryption, StringCase.Upper)]
        public string FirstName { get; set; }

        [Encrypt(Encryption.CommonDataKey, EncryptionMethod.Encryption, StringCase.Upper)]
        public string MiddleName { get; set; }

        [Encrypt(Encryption.PersonalDataKey, EncryptionMethod.Encryption, StringCase.Upper)]
        public string LastName { get; set; }

        [Encrypt(Encryption.HashingDataKey, EncryptionMethod.Hashing, StringCase.None)]
        public string Password { get; set; }
        public bool Active { get;set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
    }
}
