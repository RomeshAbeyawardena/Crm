using AutoMapper;
using Crm.Domains.Convertors;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.ViewModels;
using System;
using CustomerDto = Crm.Domains.Dto.Customer;
namespace Crm.Domains
{
    public class DomainProfile : Profile
    {
        public DomainProfile(Base64StringByteConvertor base64StringByteConvertor = default, 
            EncodingConvertor encodingConvertor = default, 
            Base64StringConvertor base64StringConvertor = default)
        {
            
            CreateMap<ConfigCryptographicCredentials, AppCryptographicCredentials>()
                .ForMember(member => member.Key, options => options.ConvertUsing(base64StringByteConvertor))
                .ForMember(member => member.InitialVector, options => options.ConvertUsing(base64StringByteConvertor))
                .ForMember(member => member.Encoding, options => options.ConvertUsing(encodingConvertor));

            CreateMap<GetCustomerViewModel, GetCustomerRequest>();
            CreateMap<CustomerDto, Customer>()
                .ReverseMap();
            CreateMap<GetCustomerRequest, CustomerDto>();
            CreateMap<GetCustomerViewModel, SearchCustomersRequest>();
            CreateMap<SearchCustomersRequest, CustomerDto>();
            CreateMap<SaveCustomerRequest, CustomerDto>().ForMember(member => member.Password, memberOptions => memberOptions.ConvertUsing(base64StringConvertor));
        }
    }
}
