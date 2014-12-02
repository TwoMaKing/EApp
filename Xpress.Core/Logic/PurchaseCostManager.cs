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
    public class PurchaseCostManager : BaseCostManager<PurchaseCostLineItem>
    {
        private readonly static PurchaseCostManager instance = new PurchaseCostManager();

        public static PurchaseCostManager Instance 
        {
            get 
            {
                return instance;
            }
        }

        public PurchaseCostManager() 
        { 
        
        }

        public override PurchaseCostLineItem CreateNewCostLineItem()
        {
            PurchaseCostLineItem newCostLine = base.CreateNewCostLineItem();

            newCostLine.ProductionSource = ProductionSource.ThirdParty;

            newCostLine.ProductionLifetime = 20;

            return newCostLine;
        }

        public override GridViewCostCellDetail GetCellDetail(PurchaseCostLineItem costLine, string columnName)
        {
            GridViewCostCellDetail costCellDetail = null;

            switch (columnName)
            {
                case CostColumnContainer.CostColumn_ProdLifeORLeaseTerm:
                    {
                        costCellDetail =
                            PurchaseCostManager.CreateDataCostCellDetail(costLine.ProductionLifetime,
                            GridViewColumnAccess.View,
                            GridViewColumnType.EditBox,
                            GridViewCellValueType.Integer);

                        costCellDetail.DisplayValue = costLine.ProductionLifetime.ToString();

                        return costCellDetail;
                    }
                default:
                    {
                        break;
                    }
            }

            return base.GetCellDetail(costLine, columnName);
        }

        protected override bool UpdateCellValue(PurchaseCostLineItem costLine, string columnName, GridViewCostCellDetail costCellDetail)
        {
            switch (columnName)
            {
                case CostColumnContainer.CostColumn_ProdLifeORLeaseTerm:
                    {
                        costLine.ProductionLifetime = costCellDetail.GetValue<int>();

                        return true;
                    }
            }

            return base.UpdateCellValue(costLine, columnName, costCellDetail);
        }

    }
}
