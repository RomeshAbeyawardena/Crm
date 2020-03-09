﻿using Crm.Domains.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Request
{
    public class VerifyCustomerCredentialsRequest : IRequest<VerifyCustomerCredentialsResponse>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
