using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpress.Core.Common;
using Xpress.Core.Entities;

namespace Xpress.Core.Logic
{
    public class HRCostManager : BaseCostManager<HRCostLineItem>
    {
        private readonly static HRCostManager instance = new HRCostManager();

        public static HRCostManager Instance
        {
            get
            {
                return instance;
            }
        }

        public HRCostManager() { }

        public override HRCostLineItem CreateNewCostLineItem()
        {
            return base.CreateNewCostLineItem();
        }

        public override GridViewCostCellDetail GetCellDetail(HRCostLineItem costLine, string columnName)
        {
            GridViewCostCellDetail costCellDetail = null;

            switch (columnName)
            {
                case CostColumnContainer.CostColumn_HRSettlement:
                    {
                        costCellDetail =
                            PurchaseCostManager.CreateDataCostCellDetail(costLine.SettlementMode,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.ComboBox,
                            GridViewCellValueType.String);

                        costCellDetail.DataSource = EntityConstants.HRSettlementModes;

                        return costCellDetail;
                    }
                default:
                    {
                        break;
                    }
            }

            return base.GetCellDetail(costLine, columnName);
        }

        protected override bool UpdateCellValue(HRCostLineItem costLine, string columnName, GridViewCostCellDetail costCellDetail)
        {
            switch (columnName)
            {
                case CostColumnContainer.CostColumn_HRSettlement:
                    {
                        HRSettlementMode settlementMode;

                        bool parseSuccess = Enum.TryParse<HRSettlementMode>(costCellDetail.GetValue<string>(), out settlementMode);

                        if (parseSuccess)
                        {
                            costLine.SettlementMode = settlementMode;
                        }

                        return parseSuccess;
                    }
            }

            return base.UpdateCellValue(costLine, columnName, costCellDetail);
        }

    }
}
