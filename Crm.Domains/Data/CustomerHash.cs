using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Data
{
    #pragma warning disable CA1819
    public class CustomerHash
    {
        [Key]
        public int Id { get; set; }
        public byte[] Hash { get; set; }
        public int Index { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
    #pragma warning restore CA1819
}
