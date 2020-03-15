using AutoMapper;
using Crm.Domains.Convertors;
using Crm.Domains.Data;
using Crm.Domains.Request;
using Crm.Domains.ViewModels;
using CustomerDto = Crm.Domains.Dto.Customer;
using CustomerAttributeDto = Crm.Domains.Dto.CustomerAttribute;

namespace Crm.Domains
{
    public class DomainProfile : Profile
    {
        private readonly Base64StringByteConvertor base64StringByteConvertor;
        private readonly EncodingConvertor encodingConvertor;
        private readonly Base64StringConvertor base64StringConvertor;

        public DomainProfile()
        {
            base64StringByteConvertor = new Base64StringByteConvertor();
            encodingConvertor = new EncodingConvertor();
            base64StringConvertor = new Base64StringConvertor();

            CreateMap<ConfigCryptographicCredentials, AppCryptographicCredentials>()
                .ForMember(member => member.Key, options => options.ConvertUsing(base64StringByteConvertor))
                .ForMember(member => member.InitialVector, options => options.ConvertUsing(base64StringByteConvertor))
                .ForMember(member => member.Encoding, options => options.ConvertUsing(encodingConvertor));

            CreateMap<CustomerDto, Customer>()
                .ForMember(member => member.EmailAddress, memberOptions => memberOptions.Ignore())
                .ForMember(member => member.FirstName, memberOptions => memberOptions.Ignore())
                .ForMember(member => member.MiddleName, memberOptions => memberOptions.Ignore())
                .ForMember(member => member.LastName, memberOptions => memberOptions.Ignore())
                .ForMember(member => member.Password, memberOptions => memberOptions.Ignore());

            CreateMap<Customer, CustomerDto>()
                .ForMember(member => member.EmailAddress, memberOptions => memberOptions.Ignore())
                .ForMember(member => member.FirstName, memberOptions => memberOptions.Ignore())
                .ForMember(member => member.MiddleName, memberOptions => memberOptions.Ignore())
                .ForMember(member => member.LastName, memberOptions => memberOptions.Ignore())
                .ForMember(member => member.Password, memberOptions => memberOptions.Ignore());

            CreateMap<VerifyCustomerCredentialsRequest, CustomerDto>().ForMember(member => member.Password, 
                memberOptions => memberOptions.ConvertUsing(base64StringConvertor));

            CreateMap<SaveCustomerRequest, CustomerDto>().ForMember(member => member.Password, 
                memberOptions => memberOptions.ConvertUsing(base64StringConvertor));

            CreateMap<CustomerAttributeDto, CustomerAttribute>().ForMember(member => member.Value, 
                options => options.Ignore()).ReverseMap();
            
        }
    }
}
