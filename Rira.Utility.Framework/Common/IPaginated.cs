using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Utility.Framework.Common
{
    public interface IPaginated<T> : IPaginated
    {
        IList<T> Data { get; }
    }


    public interface IPaginated
    {
        int CurrentPage { get; }

        bool IsFirstPage { get; }

        bool IsLastPage { get; }

        int NextPage { get; }

        int PageCount { get; }

        int PageSize { get; }

        int PreviousPage { get; }

        long TotalRowsCount { get; }
    }
}
