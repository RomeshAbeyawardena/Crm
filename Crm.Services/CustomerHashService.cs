using Crm.Contracts.Services;
using Crm.Domains.Data;
using Crm.Domains.Dto;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Options;
using DNI.Core.Services.Abstraction;
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
    public class CustomerHashService : DataServiceBase<Domains.Data.CustomerHash>, ICustomerHashService
    {
        public async Task<IEnumerable<CustomerHash>> GetCustomerHashes(int customerId, CancellationToken cancellationToken)
        {
            var query = from customerHash in DefaultQuery
                        where customerHash.CustomerId == customerId
                        select customerHash;

            return await Repository
                    .For(query)
                    .ToArrayAsync(cancellationToken);
        }

        public async Task<CustomerHash> SaveCustomerHash(CustomerHash customerHash, bool saveChanges, CancellationToken cancellationToken)
        {
            return await Repository.SaveChanges(customerHash, saveChanges, false, cancellationToken: cancellationToken);
        }

        public CustomerHash GetCustomerHash(IEnumerable<CustomerHash> customerHashes, IEnumerable<byte> hash, int atIndex)
        {
            var hashByteArray = hash.ToArray();
            var hashedString = Convert.ToBase64String(hashByteArray);
            return customerHashes.SingleOrDefault(customerHash => 
                customerHash.Index == atIndex &&
                Convert.ToBase64String(customerHash.Hash) == hashedString);
        }

        public async Task<IEnumerable<Domains.Data.Customer>> GetCustomersByHash(IEnumerable<CharacterIndex> characterIndexes, 
            CancellationToken cancellationToken,
            Action<IPagerResultOptions> configureOptions = default)
        {
            var parameterList = new List<SqlParameter>();
            var queryBuilder = new StringBuilder("DECLARE @hashValues [dbo].[Hash]\r\n\r\n");
            queryBuilder.Append("INSERT INTO @hashValues ([Value], [Index])\r\n\tVALUES\r\n\t\t");
            var parameterIndex = 0;
            foreach(var characterIndex in characterIndexes)
            {
                if(parameterIndex > 0)
                    queryBuilder.Append(",");

                queryBuilder.AppendFormat("(@p{0}, @n{0}) \r\n\t\t", parameterIndex);
                parameterList.Add(new SqlParameter("p" + parameterIndex, characterIndex.Hash.Value.ToArray()));
                parameterList.Add(new SqlParameter("n" + parameterIndex++, characterIndex.Index));
            }

            var queryString = string.Concat(queryBuilder, "\r\nEXEC [dbo].[ContainsHash] @hashes = @hashValues");

            var query = Repository
                .FromQuery<Domains.Data.Customer>(queryString, parameterList.ToArray());

            return await Repository.For(query).ToArrayAsync(cancellationToken);
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
            return await Repository.Commit(true, cancellationToken);
        }

        public CustomerHashService(IRepository<CustomerHash> customerHashRepository)
            : base(customerHashRepository, false)
        {
            
        }
    }
}
