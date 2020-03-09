using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.RequestHandler
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IMapperProvider MapperProvider;
        protected readonly IEncryptionProvider EncryptionProvider;

        public RequestHandlerBase(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider)
        {
            MapperProvider = mapperProvider;
            EncryptionProvider = encryptionProvider;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
