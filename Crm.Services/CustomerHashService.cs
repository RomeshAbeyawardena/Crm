using Crm.Contracts.Services;
using Crm.Domains.Data;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Options;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class CustomerHashService : ICustomerHashService
    {
        private readonly IRepository<CustomerHash> _customerHashRepository;

        private IQueryable<CustomerHash> DefaultQuery => _customerHashRepository.Query();

        public async Task<IEnumerable<CustomerHash>> GetCustomerHashes(int customerId, CancellationToken cancellationToken)
        {
            var query = from customerHash in DefaultQuery
                        where customerHash.CustomerId == customerId
                        select customerHash;

            return await _customerHashRepository
                    .For(query)
                    .ToArrayAsync(cancellationToken);
        }

        public async Task<CustomerHash> SaveCustomerHash(CustomerHash customerHash, bool saveChanges, CancellationToken cancellationToken)
        {
            return await _customerHashRepository.SaveChanges(customerHash, saveChanges, false, cancellationToken: cancellationToken);
        }

        public CustomerHash GetCustomerHash(IEnumerable<CustomerHash> customerHashes, IEnumerable<byte> hash)
        {
            var hashByteArray = hash.ToArray();
            var hashedString = Convert.ToBase64String(hashByteArray);
            return customerHashes.SingleOrDefault(customerHash => Convert.ToBase64String(customerHash.Hash) == hashedString);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByHash(IEnumerable<IEnumerable<byte>> hashes, 
            CancellationToken cancellationToken,
            Action<IPagerResultOptions> configureOptions = default)
        {
            var hashBytes = hashes.Select(h => h.ToArray());
            var parameterList = new List<SqlParameter>();
            var queryBuilder = new StringBuilder("DECLARE @hashValues [dbo].[Hash]\r\n\r\nINSERT INTO @hashValues ([Value])\r\n\tVALUES\r\n\t\t");
            var parameterIndex = 0;
            foreach(var hashByte in hashBytes)
            {
                if(parameterIndex > 0)
                    queryBuilder.Append(",");

                queryBuilder.AppendFormat("(@p{0}) \r\n\t\t", parameterIndex);
                parameterList.Add(new SqlParameter("p" + parameterIndex++, hashByte));
            }

            var queryString = string.Concat(queryBuilder, "\r\nEXEC [dbo].[ContainsHash] @hashes = @hashValues");

            var query = _customerHashRepository.FromQuery<Customer>(queryString, parameterList.ToArray());

            return await _customerHashRepository.For(query).ToArrayAsync(cancellationToken);
            //if(configureOptions == null)
            //    return await _customerHashRepository
            //            .For(query)
            //            .ToArrayAsync(cancellationToken);

            //var pager =_customerHashRepository
            //    .For(query)
            //    .AsPager();

            //return await pager.GetPagedItems(configureOptions, cancellationToken);
        }

        public async Task<int> CommitChanges(CancellationToken cancellationToken)
        {
            return await _customerHashRepository.Commit(true, cancellationToken);
        }

        public CustomerHashService(IRepository<CustomerHash> customerHashRepository)
        {
            _customerHashRepository = customerHashRepository;
        }
    }
}
