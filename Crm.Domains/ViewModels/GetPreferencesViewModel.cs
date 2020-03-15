using AutoMapper;
using Crm.Domains.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.ViewModels
{
    [AutoMap(typeof(GetPreferencesRequest))]
    public class GetPreferencesViewModel
    {
        public string CategoryName { get; set; }
    }
}
