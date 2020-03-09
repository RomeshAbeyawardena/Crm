﻿using Crm.Domains.Constants;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Data
{
    public class CustomerAttribute
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public int CustomerId { get; set; }
        [Encrypt(Encryption.PersonalDataKey, EncryptionMethod.Encryption, StringCase.None)]
        public byte[] Value { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
    }
}