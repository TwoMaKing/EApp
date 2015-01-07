using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Query
{
    /// <summary>
    /// 为 IEnumerable<> 定义一个扩展方法 叫SortBy
    /// </summary>
    public static class SortByExtension
    {
        public static IOrderedEnumerable<TSource> SortBy<TSource, TKey>(
            this IEnumerable<TSource> query,
            Func<TSource, TKey> sortPredicate,
            SortOrder sortOrder)
        {
            return SortBy(query.AsQueryable(), sortPredicate, sortOrder);
        }

        public static IOrderedEnumerable<TSource> SortBy<TSource, TKey>(
            this IQueryable<TSource> query,
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
