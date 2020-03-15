using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;

namespace Crm.Domains.Data
{
    [MessagePack.MessagePackObject(true)]
    public class Attribute
    {
        public int Id { get; set; }
        public string Key { get; set; }
        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
        [Modifier(ModifierFlag.Created | ModifierFlag.Modified)]
        public DateTimeOffset Modified { get; set; }
    }
}
