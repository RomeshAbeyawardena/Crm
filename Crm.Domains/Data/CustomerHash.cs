using System.ComponentModel.DataAnnotations;

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
