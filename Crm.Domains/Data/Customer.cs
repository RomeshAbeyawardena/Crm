using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Data
{
    public class Customer
    {
        #pragma warning disable CA1819
        public int Id { get; set; }
        public byte[] EmailAddress { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] MiddleName { get; set; }
        public byte[] LastName { get; set; }
        public bool Active { get; set; }
#pragma warning restore CA1819
    }
}
