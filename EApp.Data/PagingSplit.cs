using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EApp.Data
{
    public class PagingSplit : IPagingSplit
    {
        public Database Database
        {
            get { throw new NotImplementedException(); }
        }

        public int? TotalRecords
        {
            get { throw new NotImplementedException(); }
        }

        public int? TotalPages
        {
            get { throw new NotImplementedException(); }
        }

        public int? PageNumber
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int? PageSize
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Where
        {
            get { throw new NotImplementedException(); }
        }

        public string OrderBy
        {
            get { throw new NotImplementedException(); }
        }

        public object[] ParamValues
        {
            get { throw new NotImplementedException(); }
        }

        public DataSet GetPagingData(int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetPagingDataReadOnly(int pageNumber)
        {
            throw new NotImplementedException();
        }

    }
}
