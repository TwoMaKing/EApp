using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.QuerySepcifications
{
    /// <summary>
    /// 用于查询 query 返回 Data Transfer Object (DTO)
    /// </summary>
    public interface IFinder<TDTO>
    {
        /// <summary>
        /// Query all of elements.
        /// </summary>
        IQueryable<TDTO> FindAll();

        /// <summary>
        /// Paging query.
        /// </summary>
        IQueryable<TDTO> FindAll(int pageIndex, int pageCount);


        /// <summary>
        /// Query all of elements by the Lamda expression.
        /// </summary>
        IQueryable<TDTO> FindAll(Func<TDTO, bool> query);

        /// <summary>
        /// Paging query by by the Lamda expression.
        /// </summary>
        IQueryable<TDTO> FindAll(Func<TDTO, bool> query, int pageIndex, int pageCount);

    }
}
