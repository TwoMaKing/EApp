using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.QueryPaging;

namespace EApp.Data
{
    public interface IPagingSplit : IQueryPaging
    {
        DataSet GetPagingData(int pageNumber);

        IDataReader GetPagingDataReadOnly(int pageNumber);

        Database Database { get; }

        string Where { get; }

        string OrderBy { get; }

        object[] ParamValues { get; }
    }
}
