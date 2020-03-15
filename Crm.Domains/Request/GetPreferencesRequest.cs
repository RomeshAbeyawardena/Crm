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
    [AutoMap(typeof(GetPreferencesViewModel))]
    public class GetPreferencesRequest : IRequest<GetPreferencesResponse>
    {
        public string CategoryName { get; set; }
    }
}
