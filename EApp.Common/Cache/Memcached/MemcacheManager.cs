using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Cache
{
    public class MemcacheManager : ICacheManager
    {
        public void AddItem(string key, object obj)
        {
            throw new NotImplementedException();
        }

        public void AddItem(string key, object obj, int expire)
        {
            throw new NotImplementedException();
        }

        public void AddItem<T>(string key, T item)
        {
            throw new NotImplementedException();
        }

        public void AddItem<T>(string key, T item, int expire)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public object GetItem(string key)
        {
            throw new NotImplementedException();
        }

        public T GetItem<T>(string key)
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
