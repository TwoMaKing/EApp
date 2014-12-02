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
    public class MaintenanceCostManager : BaseCostManager<MaintenanceCostLineItem>
    {
        private readonly static MaintenanceCostManager instance = new MaintenanceCostManager();

        public static MaintenanceCostManager Instance
        {
            get
            {
                return instance;
            }
        }

        public MaintenanceCostManager() { }

        public override MaintenanceCostLineItem CreateNewCostLineItem()
        {
            MaintenanceCostLineItem newCostLine = base.CreateNewCostLineItem();

            newCostLine.ServiceAmount = 1000;
            
            return newCostLine;
        }

        public override GridViewCostCellDetail GetCellDetail(MaintenanceCostLineItem costLine, string columnName)
        {
            GridViewCostCellDetail costCellDetail = null;

            switch (columnName)
            {
                case CostColumnContainer.CostColumn_ServiceAmount:
                    {
                        costCellDetail =
                            PurchaseCostManager.CreateDataCostCellDetail(costLine.ServiceAmount,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.EditBox,
                            GridViewCellValueType.Decimal);

                        if (costLine.ServiceAmount.HasValue)
                        {
                            costCellDetail.DisplayValue = costLine.ServiceAmount.Value.ToString("0.00");
                        }

                        return costCellDetail;
                    }
            }

            return base.GetCellDetail(costLine, columnName);
        }

        protected override bool UpdateCellValue(MaintenanceCostLineItem costLine, string columnName, GridViewCostCellDetail costCellDetail)
        {
            switch (columnName)
            {
                case CostColumnContainer.CostColumn_ServiceAmount:
                    {
                        costLine.ServiceAmount = costCellDetail.GetValue<decimal>();
                        
                        return true;
                    }
            }

            return base.UpdateCellValue(costLine, columnName, costCellDetail);
        }
    }
}
