using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.ViewModels
{
    public interface ICustomerViewModel
    {
        int? Id { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
    }
}
