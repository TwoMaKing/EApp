using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;
using Xpress.Core.Common;

namespace Xpress.Core.Entities
{
    public class GridViewCellDetail<TEntityItem> where TEntityItem : IEntity<Guid>
    {
        public GridViewColumnAccess AccessStatus { get; set; }

        public GridViewColumnType ColumnType { get; set; }

        public GridViewCellValueType ValueType { get; set; }

        public TEntityItem CurrentEnitiy { get; set; }

        public object Value { get; set; } 

        public string DisplayValue { get; set; }

        public TValue GetValue<TValue>()
        {
            if (this.Value is TValue)
            {
                return (TValue)this.Value;
            }

            return default(TValue);
        }
    }

    public class GridViewCostCellDetail : GridViewCellDetail<CostLineItemBase> 
    {
        public string[][] DataSource { get; set; }
    }

}
