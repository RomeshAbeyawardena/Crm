using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;

namespace Crm.Domains.Data
{
    [MessagePack.MessagePackObject(true)]
    public class Preference
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        [Modifier(ModifierFlag.Created | ModifierFlag.Modified)]
        public DateTimeOffset Modified { get; set; }

        public virtual Category Category { get; set; }
    }
}
