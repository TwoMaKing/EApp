using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Query
{
    public interface IQuery<T> where T : class
    {
        IList<T> ToList(IEnumerable<T> querySource);

        IPagingResult<T> ToPagedList(IEnumerable<T> querySource, int pageNumber, int pageSize);
    }

}
