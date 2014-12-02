using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common.Mapper;
using EApp.Core;
using Xpress.Core.Common;
using Xpress.Core.Entities;
using EApp.Common.Util;

namespace Xpress.Core.Logic
{
    public class LeaseCostManager : BaseCostManager<LeaseCostLineItem>
    {
        private readonly static LeaseCostManager instance = new LeaseCostManager();

        public static LeaseCostManager Instance
        {
            get
            {
                return instance;
            }
        }

        public LeaseCostManager() { }

        public override LeaseCostLineItem CreateNewCostLineItem()
        {
            return base.CreateNewCostLineItem();
        }

        public override GridViewCostCellDetail GetCellDetail(LeaseCostLineItem costLine, string columnName)
        {
            GridViewCostCellDetail costCellDetail = null;

            switch (columnName)
            {
                case CostColumnContainer.CostColumn_LeaseRateFactor:
                    {
                        costCellDetail =
                            PurchaseCostManager.CreateDataCostCellDetail(costLine.LeaseRateFactor,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.EditBox,
                            GridViewCellValueType.Percentage);

                        costCellDetail.DisplayValue = LocalizationUtil.FormatRateToString(costLine.LeaseRateFactor, 6);

                        return costCellDetail;
                    }
                case CostColumnContainer.CostColumn_CompliantLeaseOption:
                    {
                        costCellDetail =
                            PurchaseCostManager.CreateDataCostCellDetail(costLine.CompliantLeaseOption,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.ComboBox,
                            GridViewCellValueType.String);

                        costCellDetail.DataSource = EntityConstants.CompliantLeaseOptions;

                        return costCellDetail;
                    }
                default:
                    {
                        break;
                    }
            }

            return base.GetCellDetail(costLine, columnName);
        }

        protected override bool UpdateCellValue(LeaseCostLineItem costLine, string columnName, GridViewCostCellDetail costCellDetail)
        {
            switch (columnName)
            {
                case CostColumnContainer.CostColumn_LeaseRateFactor:
                    {
                        costLine.LeaseRateFactor = costCellDetail.GetValue<decimal>();

                        return true;
                    }
                case CostColumnContainer.CostColumn_CompliantLeaseOption:
                    {
                        CompliantLeaseOption compliantLeaseOption;

                        bool parseSuccess = Enum.TryParse<CompliantLeaseOption>(costCellDetail.GetValue<string>(), out compliantLeaseOption);

                        if (parseSuccess)
                        {
                            costLine.CompliantLeaseOption = compliantLeaseOption;
                        }

                        return parseSuccess;
                    }
            }

            return base.UpdateCellValue(costLine, columnName, costCellDetail);
        }
    }
}
