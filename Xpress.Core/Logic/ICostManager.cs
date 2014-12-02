using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common.Mapper;
using EApp.Core;
using Xpress.Core.Common;
using Xpress.Core.Entities;

namespace Xpress.Core.Logic
{
    public interface ICostManager
    {
        IEnumerable<CostLineItemBase> CurrentDisplayedCostLines { get; }

        void InitializeAllCostLines();

        GridViewCostCellDetail GetCellDetail(CostLineItemBase costLine, string columnName);

        bool UpdateCellValue(CostLineItemBase costLine, string columnName, object newValue);

        void AddCostLine();

        void CopyCostLine(CostLineItemBase costLine);

        void DeleteCostLine(int costLineId);

        void Save();
    }

}
