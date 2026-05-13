using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Utility.Framework.Common
{
    public class PaginatedCriteria : IPaginatedCriteria
    {
        public PaginatedCriteria(int currentPage = 0, int pageSize = 10)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int StartIndex => (this.CurrentPage - 1) * this.PageSize;
    }
}
