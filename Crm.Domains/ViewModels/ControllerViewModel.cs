using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.ViewModels
{
    public class ControllerViewModel
    {
        IEnumerable<ActionViewModel> Actions { get; set; }
        public string Name { get; set; }
    }
}
