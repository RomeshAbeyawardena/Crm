using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Crm.Domains.ViewModels
{
    public class GetCustomerAttributeViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }
    }
}
