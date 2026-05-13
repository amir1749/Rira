using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Utility.Framework.Domain
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public byte[]? RowVersion {  get; set; }
    }
}
