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
    public class LaborCostManager : BaseCostManager<LaborCostLineItem>
    {
        private readonly static LaborCostManager instance = new LaborCostManager();

        public static LaborCostManager Instance
        {
            get
            {
                return instance;
            }
        }

        public LaborCostManager() { }

        public override LaborCostLineItem CreateNewCostLineItem()
        {
            LaborCostLineItem newCostLine = base.CreateNewCostLineItem();

            newCostLine.JobTypeId = 1000;
            
            return newCostLine;
        }

        public override GridViewCostCellDetail GetCellDetail(LaborCostLineItem costLine, string columnName)
        {
            GridViewCostCellDetail costCellDetail = null;

            switch (columnName)
            {
                case CostColumnContainer.CostColumn_JobType:
                    {
                        costCellDetail =
                            PurchaseCostManager.CreateDataCostCellDetail(costLine.JobTypeId,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.ComboBox,
                            GridViewCellValueType.Integer);

                        costCellDetail.DataSource = XpressTestingFakeData.JobTypeDataSource;

                        return costCellDetail;
                    }
            }

            return base.GetCellDetail(costLine, columnName);
        }

        protected override bool UpdateCellValue(LaborCostLineItem costLine, string columnName, GridViewCostCellDetail costCellDetail)
        {
            switch (columnName)
            {
                case CostColumnContainer.CostColumn_JobType:
                    {
                        costLine.JobTypeId = costCellDetail.GetValue<int>();

                        return true;
                    }
            }

            return base.UpdateCellValue(costLine, columnName, costCellDetail);
        }
    }
}
