using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Utility.Framework.Common
{
    public class Paginated<T> : IPaginated<T>
    {
        public Paginated(IList<T> data, long totalItemCount, int currentPage, int pageSize)
        {
            this.TotalRowsCount = totalItemCount;
            this.PageSize = pageSize;
            this.CurrentPage = currentPage;
            this.Data = data;
        }

        public Paginated(IList<T> data, long totalItemCount)
        {
            this.TotalRowsCount = totalItemCount;
            this.Data = data;
        }

        public IList<T> Data { get; }

        public long TotalRowsCount { get; }

        public int PageSize { get; }

        public int CurrentPage { get; }

        public int PageCount => (int)Math.Ceiling((double)this.TotalRowsCount / (double)this.PageSize);


        public int PreviousPage => this.CurrentPage > 1 ? this.CurrentPage - 1 : 1;


        public int NextPage => this.CurrentPage < this.PageCount ? this.CurrentPage + 1 : this.CurrentPage;


        public bool IsFirstPage => this.CurrentPage <= 1;


        public bool IsLastPage => this.CurrentPage >= this.PageCount;
    }
}
