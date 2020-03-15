﻿using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Data
{
    public class CustomerPreference
    {
        [Key]
        public int Id { get; set; }
        public int PreferenceId { get; set; }
        public int CustomerId { get; set; }
        
        public DateTimeOffset? OptInDate { get; set; }
        public DateTimeOffset Expiry { get; set; }
        
        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        [Modifier(ModifierFlag.Created | ModifierFlag.Modified)]
        public DateTimeOffset Modified { get; set; }

        public virtual Preference Preference { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
