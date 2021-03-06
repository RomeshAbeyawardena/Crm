﻿using DNI.Core.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.ViewModels
{
    public class SavePreferenceViewModel
    {
        [Required]
        public string Name { get; set; }

        [Optional(nameof(CategoryId), nameof(CategoryName))]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
