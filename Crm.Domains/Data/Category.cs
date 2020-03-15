using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Crm.Domains.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        [Modifier(ModifierFlag.Created | ModifierFlag.Modified)]
        public DateTimeOffset Modified { get; set; }
    }
}
