﻿using Crm.Domains.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Notifications
{
    public class SaveCustomerNotification : INotification
    {
        public Customer SavedCustomer { get; set; }
    }
}