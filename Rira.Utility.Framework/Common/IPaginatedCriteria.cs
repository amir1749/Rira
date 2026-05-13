using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Utility.Framework.Common
{
    public interface IPaginatedCriteria
    {
        int CurrentPage { get; set; }

        int PageSize { get; set; }

        int StartIndex { get; }
    }
}
