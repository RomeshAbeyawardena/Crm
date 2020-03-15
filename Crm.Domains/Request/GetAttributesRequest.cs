using AutoMapper;
using Crm.Domains.Response;
using Crm.Domains.ViewModels;
using MediatR;

namespace Crm.Domains.Request
{
    [AutoMap(typeof(GetAttributesViewModel))]
    public class GetAttributesRequest : IRequest<GetAttributesResponse>
    {
        
    }
}
