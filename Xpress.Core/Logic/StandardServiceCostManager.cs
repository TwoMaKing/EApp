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
    public class StandardServiceCostManager : BaseCostManager<StandardServiceCostLineItem>
    {
        private readonly static StandardServiceCostManager instance = new StandardServiceCostManager();

        public static StandardServiceCostManager Instance
        {
            get
            {
                return instance;
            }
        }

        public StandardServiceCostManager() { }

        public override StandardServiceCostLineItem CreateNewCostLineItem()
        {
            StandardServiceCostLineItem newCostLine = base.CreateNewCostLineItem();

            newCostLine.InvoicingTreatment = InvoicingTreatment.Spread;

            return newCostLine;
        }

        public override GridViewCostCellDetail GetCellDetail(StandardServiceCostLineItem costLine, string columnName)
        {
            GridViewCostCellDetail costCellDetail = null;

            switch (columnName)
            {
                case CostColumnContainer.CostColumn_InvTreat:
                    {
                        costCellDetail =
                            PurchaseCostManager.CreateDataCostCellDetail(costLine.InvoicingTreatment,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.ComboBox,
                            GridViewCellValueType.String);

                        costCellDetail.DataSource = EntityConstants.InvoicingTreatments;

                        return costCellDetail;
                    }
            }

            return base.GetCellDetail(costLine, columnName);
        }

        protected override bool UpdateCellValue(StandardServiceCostLineItem costLine, string columnName, GridViewCostCellDetail costCellDetail)
        {
            switch (columnName)
            {
                case CostColumnContainer.CostColumn_InvTreat:
                    {
                        InvoicingTreatment invoicingTreatment;

                        bool parseSuccess = Enum.TryParse<InvoicingTreatment>(costCellDetail.GetValue<string>(), out invoicingTreatment);

                        if (parseSuccess)
                        {
                            costLine.InvoicingTreatment = invoicingTreatment;
                        }

                        return parseSuccess;
                    }
            }

            return base.UpdateCellValue(costLine, columnName, costCellDetail);
        }
    }
}
