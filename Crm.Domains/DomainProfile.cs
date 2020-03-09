﻿using AutoMapper;
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
        private readonly Base64StringConvertor _base64StringConvertor;
        private readonly EncodingConvertor _encodingConvertor;
        public DomainProfile()
        {
            _base64StringConvertor = new Base64StringConvertor();
            _encodingConvertor = new EncodingConvertor();

            CreateMap<ConfigCryptographicCredentials, AppCryptographicCredentials>()
                .ForMember(member => member.Key, options => options.ConvertUsing(_base64StringConvertor))
                .ForMember(member => member.InitialVector, options => options.ConvertUsing(_base64StringConvertor))
                .ForMember(member => member.Encoding, options => options.ConvertUsing(_encodingConvertor));

            CreateMap<GetCustomerViewModel, GetCustomerRequest>();
            CreateMap<CustomerDto, Customer>()
                .ReverseMap();
            CreateMap<GetCustomerRequest, CustomerDto>();
            CreateMap<GetCustomerViewModel, SearchCustomersRequest>();
            CreateMap<SearchCustomersRequest, CustomerDto>();
            CreateMap<SaveCustomerRequest, CustomerDto>();
        }
    }
}
