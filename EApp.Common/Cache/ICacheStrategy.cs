using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Cache
{
    public interface ICacheStrategy
    {
        /// <summary>
        /// 添加对象到缓存
        /// </summary>
        void AddItem(string key, object obj);

        /// <summary>
        /// 添加对象到缓存指定到期时间
        /// </summary>
        void AddItem(string key, object obj, int expire);

        /// <summary>
        /// 获取对象 By Key
        /// </summary>
        object GetItem(string key);
        
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
