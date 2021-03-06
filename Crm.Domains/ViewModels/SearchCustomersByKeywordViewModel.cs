﻿using Crm.Domains.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Crm.Domains.ViewModels
{
    public class SearchCustomersByKeywordViewModel : IPagedRequest
    {
        [Required]
        public string Keyword { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        public int MaximumRowsPerPage { get; set; }
    }
}
