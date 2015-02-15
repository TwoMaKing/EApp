using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{
    public class DbSet : DbQuery, IDbSet
    {
        public object Get(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(object entity)
        {
            throw new NotImplementedException();
        }

        public void Update(object entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
