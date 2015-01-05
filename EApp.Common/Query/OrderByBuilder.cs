using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Query
{
    public class OrderByBuilder
    {
        private List<OrderByItem> orderByItems = new List<OrderByItem>();

        public List<OrderByItem> OrderByItems 
        {
            get 
            {
                return this.orderByItems;
            }
        }

        public override string ToString()
        {
            return string.Join(",", orderByItems.Select(o => o.ToString()).ToArray());
        }

        public void Add(string propertyName, SortOrder direction) 
        {
            this.Add(new OrderByItem(propertyName, direction));
        }

        public void Add(OrderByItem item)
        {
            this.orderByItems.Add(item);
        }

    }
}
