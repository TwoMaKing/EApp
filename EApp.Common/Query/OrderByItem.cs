using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Query
{
    public class OrderByItem
    {
        public OrderByItem(string name, SortOrder direction)
        {
            this.Name = name;

            this.Direction = direction;
        }

        /// <summary>
        /// 排序属性
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        public SortOrder Direction { get; private set; }

        /// <summary>
        /// 创建排序字符串
        /// </summary>
        public override string ToString()
        {
            if (this.Direction == SortOrder.Ascending ||
                this.Direction == SortOrder.None)
                return this.Name;

            return string.Format("{0} desc", this.Name);
        }
    }
}
