using AutoMapper.Configuration.Annotations;
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
        [Ignore]
        public byte[] EmailAddress { get; set; }
        [Ignore]
        public byte[] FirstName { get; set; }
        [Ignore]
        public byte[] MiddleName { get; set; }
        [Ignore]
        public byte[] LastName { get; set; }
        public bool Active { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        [Modifier(ModifierFlag.Modified)]
        public DateTimeOffset Modified { get; set; }
#pragma warning restore CA1819
    }
}
