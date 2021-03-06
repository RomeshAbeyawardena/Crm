﻿using AutoMapper;
using Crm.Domains.Response;
using Crm.Domains.ViewModels;
using MediatR;

namespace Crm.Domains.Request
{
    [AutoMap(typeof(SearchCustomerViewModel))]
    public class SearchCustomersRequest : IRequest<SearchCustomersResponse>
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int PageNumber { get; set; }
        public int MaximumRowsPerPage { get; set; }
    }
}
