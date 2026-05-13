using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Core.Domain.Common.Exception
{
    public class DomainException : System.Exception
    {
        public DomainException(string message)
       : base(message)
        {
        }
    }
}
