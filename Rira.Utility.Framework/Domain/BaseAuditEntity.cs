using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rira.Utility.Framework.Domain
{
    public  class BaseAuditEntity : BaseEvent
    {
        [Key]
        public Guid Id { get; set; }

        public byte[]? RowVersion { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime ModifyDatetime { get; set; }

        public Guid CreatorId { get; set; }

        public Guid ModifierId { get; set; }

    }
}
