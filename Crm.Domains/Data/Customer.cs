using AutoMapper.Configuration.Annotations;
using Crm.Domains.Constants;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Data
{
    public class Customer
    {
        #pragma warning disable CA1819
        [Key]
        public int Id { get; set; }

        [Encrypt(Encryption.IdentificationKey, EncryptionMethod.Encryption, StringCase.Upper)]
        public byte[] EmailAddress { get; set; }
        
        [Encrypt(Encryption.CommonDataKey, EncryptionMethod.Encryption, StringCase.Upper)]
        public byte[] FirstName { get; set; }
        
        [Encrypt(Encryption.CommonDataKey, EncryptionMethod.Encryption, StringCase.Upper)]
        public byte[] MiddleName { get; set; }
        
        [Encrypt(Encryption.PersonalDataKey, EncryptionMethod.Encryption, StringCase.Upper)]
        public byte[] LastName { get; set; }

        [Encrypt(Encryption.HashingDataKey, EncryptionMethod.Hashing, StringCase.None)]
        public byte[] Password { get; set; }

        public bool Active { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        [Modifier(ModifierFlag.Created | ModifierFlag.Modified)]
        public DateTimeOffset Modified { get; set; }

        public DateTimeOffset? LastIndexed { get; set; }
#pragma warning restore CA1819
    }
}
