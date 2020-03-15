using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Data
{
    public class CustomerNotification
    {
        [Key]
        public int Id { get; set; }

        public int NotificationId { get; set; }

        public int CustomerId { get; set; }

        public DateTimeOffset? SentDate { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        [Modifier(ModifierFlag.Created | ModifierFlag.Modified)]
        public DateTimeOffset Modified { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Notification Notification { get; set; }
    }
}
