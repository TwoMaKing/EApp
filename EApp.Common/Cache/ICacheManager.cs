using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Cache
{
    public interface ICacheManager
    {
        /// <summary>
        /// 添加对象到缓存
        /// </summary>
        void AddItem(string key, object item);

        /// <summary>
        /// 添加对象到缓存指定到期时间
        /// </summary>
        void AddItem(string key, object item, int expire);

        /// <summary>
        /// 添加对象到缓存
        /// </summary>
        void AddItem<T>(string key, T item);

        /// <summary>
        /// 添加对象到缓存指定到期时间
        /// </summary>
        void AddItem<T>(string key, T item, int expire);

        /// <summary>
        /// 是否存在指定Key的Item
        /// </summary>
        bool ContainsKey(string key);

        /// <summary>
        /// 获取对象 By Key
        /// </summary>
        object GetItem(string key);

        /// <summary>
        /// 获取对象 By Key
        /// </summary>
        T GetItem<T>(string key);

        /// <summary>
        /// 从缓存中移除
        /// </summary>
        void RemoveItem(string key);

        /// <summary>
        /// 清空所有缓存对象
        /// </summary>
        void FlushAll();
    }
}
