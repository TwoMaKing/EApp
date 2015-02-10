using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Query
{
    /// <summary>
    /// Query paging which contains a set of objects that is from
    /// paging split of the entire object set.
    /// </summary>
    public interface IQueryPaging
    {
        int? TotalRecords { get; }

        int? TotalPages { get; }

        int? PageNumber { get; }

        int? PageSize { get; }
    }
}
