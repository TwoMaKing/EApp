using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common.Query;
using EApp.Core.Query;

namespace Xpress.Mvc.Logic
{
    public static class SortByExtension
    {
        public static IOrderedEnumerable<TSource> SortBy<TSource, TKey>(
            this IEnumerable<TSource> query, 
            Func<TSource, TKey> sortPredicate, 
            SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending ||
                sortOrder == SortOrder.None)
            {
                return query.OrderBy(sortPredicate);
            }
            else
            {
                return query.OrderByDescending(sortPredicate);
            }
        }
    }
}
