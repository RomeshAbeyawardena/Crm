using Crm.Domains.Request;
using Crm.Domains.Response;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services.RequestHandler
{
    public class VerifyCustomerCredentials : RequestHandlerBase<VerifyCustomerCredentialsRequest, VerifyCustomerCredentialsResponse>
    {
        public VerifyCustomerCredentials(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider) 
            : base(mapperProvider, encryptionProvider)
        {
        }

        public override Task<VerifyCustomerCredentialsResponse> Handle(VerifyCustomerCredentialsRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
