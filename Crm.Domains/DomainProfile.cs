using AutoMapper;
using Crm.Domains.Request;
using Crm.Domains.ViewModels;
using System;

namespace Crm.Domains
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<GetCustomerViewModel, GetCustomerRequest>();
        }
    }
}
