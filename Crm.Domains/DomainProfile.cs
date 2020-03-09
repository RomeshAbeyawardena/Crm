using AutoMapper;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.ViewModels;
using System;
using CustomerDto = Crm.Domains.Dto.Customer;
namespace Crm.Domains
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<GetCustomerViewModel, GetCustomerRequest>();
            CreateMap<CustomerDto, Customer>()
                .ReverseMap();
            CreateMap<GetCustomerRequest, CustomerDto>();
            CreateMap<SearchCustomersRequest, CustomerDto>();
            CreateMap<SaveCustomerRequest, CustomerDto>();
        }
    }
}
