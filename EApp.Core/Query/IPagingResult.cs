using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Query
{
    /// <summary>
    /// Paging result which contains a set of objects that is from
    /// paging split of the entire object set.
    /// </summary>
    public interface IPagingResult<T> : IQueryPaging
    {
        IEnumerable<T> Data { get; }
    }
}
