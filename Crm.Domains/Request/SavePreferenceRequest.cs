using AutoMapper;
using Crm.Domains.Response;
using Crm.Domains.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Request
{
    [AutoMap(typeof(SavePreferenceViewModel))]
    public class SavePreferenceRequest : IRequest<SavePreferenceResponse>
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
