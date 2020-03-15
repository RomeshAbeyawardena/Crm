using System.ComponentModel.DataAnnotations;

namespace Crm.Domains.ViewModels
{
    public class SaveCustomerAttributeViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string Property { get; set; }
        public string Value { get; set; }
    }
}
