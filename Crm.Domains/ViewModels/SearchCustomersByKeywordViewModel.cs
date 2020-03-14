using Crm.Domains.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
