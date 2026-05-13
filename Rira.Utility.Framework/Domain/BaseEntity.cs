using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rira.Utility.Framework.Domain
{
    public class BaseEntity : BaseEvent
    {
        [Key]
        public Guid Id { get; set; }
    }
}
