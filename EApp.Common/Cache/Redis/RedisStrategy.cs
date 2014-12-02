using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Cache
{
    public class RedisStrategy : ICacheStrategy
    {
        public void AddItem(string key, object obj)
        {
            throw new NotImplementedException();
        }

        public void AddItem(string key, object obj, int expire)
        {
            throw new NotImplementedException();
        }

        public object GetItem(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string key)
        {
            throw new NotImplementedException();
        }

        public void FlushAll()
        {
            throw new NotImplementedException();
        }
    }
}
