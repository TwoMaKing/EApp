using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common.Mapper;
using EApp.Core;
using EApp.UI.Controls.GridView;
using Xpress.Core.Common;
using Xpress.Core.Entities;

namespace Xpress.Core.Logic
{
    /// <summary>
    /// The base logic for Cost module including initialize all of cost lines, add/copy/delete/save/find cost line,
    /// and display the each field (i.e. property) value on data grid view cell.
    /// and update the each field (i.e. property) value by editing data grid view cell
    /// </summary>
    public abstract class BaseCostManager<TCostLineItem> : ICostManager where TCostLineItem : CostLineItemBase
    {
        private List<TCostLineItem> allCostLines = new List<TCostLineItem>();

        private List<TCostLineItem> currentDisplayedCostLines = new List<TCostLineItem>();

        public BaseCostManager() { }

        public IEnumerable<TCostLineItem> CurrentDisplayedCostLines
        {
            get 
            {
                return this.currentDisplayedCostLines;
            }
        }

        public virtual void InitializeAllCostLines()
        {
            // make fake data

            allCostLines.Clear();

            for (int i = 0; i < 5; i++) 
            {
                TCostLineItem newCostLine = this.CreateNewCostLineItem();
                newCostLine.Id = 1000 + i;
                newCostLine.GroupId = XpressTestingFakeData.Groups[i].Id;
                newCostLine.SubgroupId = XpressTestingFakeData.Groups[i].Subgroups[i].Id;
                newCostLine.TotalCost = 1000 + i * 50;
                newCostLine.TotalPrice = 2000 + i * 50;
                this.allCostLines.Add(newCostLine);
            }


            FilterCostLine(null);
        }

        public void FilterCostLine(Func<TCostLineItem, bool> predicate) 
        {
            this.currentDisplayedCostLines.Clear();

            if (predicate == null)
            {
                this.currentDisplayedCostLines.AddRange(allCostLines);
            }
            else
            {
                this.currentDisplayedCostLines.AddRange(allCostLines.Where<TCostLineItem>(predicate));
            }
        }

        public void AddCostLine() 
        {
            TCostLineItem newCostLine = this.CreateNewCostLineItem();

            this.allCostLines.Add(newCostLine);

            FilterCostLine(null);
        }

        public void CopyCostLine(TCostLineItem costLine) 
        {
            TCostLineItem copyOfCostLine = this.CopyCostLineItem(costLine);

            this.allCostLines.Add(copyOfCostLine);
        }

        public virtual void DeleteCostLine(int costLineId) 
        {
            this.allCostLines.RemoveAll(new Predicate<TCostLineItem>(cost => cost.Id.Equals(costLineId)));
        }

        public void Save() 
        { 
            
        }

        /// <summary>
        /// Get the GridViewCostCellDetail instance for the each property in the cost line.
        /// and then show the value of the GridViewCostCellDetail instance on the data grid view cell.
        /// </summary>
        public virtual GridViewCostCellDetail GetCellDetail(TCostLineItem costLine, string columnName) 
        {
            GridViewCostCellDetail costCellDetail = null;

            switch (columnName)
            {
                case CostColumnContainer.CostColumn_ID:
                    {
                        costCellDetail =
                            BaseCostManager<TCostLineItem>.CreateDataCostCellDetail(costLine.Id,
                            GridViewColumnAccess.View,
                            GridViewColumnType.EditBox,
                            GridViewCellValueType.Integer);

                        costCellDetail.DisplayValue = costLine.Id.ToString();

                        return costCellDetail;
                    }
                case CostColumnContainer.CostColumn_Selection:
                    {
                        costCellDetail =
                            BaseCostManager<TCostLineItem>.CreateDataCostCellDetail(costLine.Checked,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.CheckBox,
                            GridViewCellValueType.Boolean);

                        return costCellDetail;
                    }
                case CostColumnContainer.CostColumn_Status:
                    {
                        string imageName = EntityConstants.Status_Current;

                        if (costLine.ChangeStauts == ChangeStatus.New)
                        { 
                            imageName = EntityConstants.Status_New;
                        }
                        else if (costLine.ChangeStauts == ChangeStatus.Modification)
                        {
                            imageName = EntityConstants.Status_Edit;
                        }
                        else if (costLine.ChangeStauts == ChangeStatus.Deletion)
                        {
                            imageName = EntityConstants.Status_Deletion;
                        }

                        costCellDetail =
                            BaseCostManager<TCostLineItem>.CreateDataCostCellDetail(new ValueImage("Plugin", imageName),
                            GridViewColumnAccess.View,
                            GridViewColumnType.IconBox,
                            GridViewCellValueType.Icon);

                        return costCellDetail;
                    }
                case CostColumnContainer.CostColumn_Group:
                    {
                        costCellDetail =
                            BaseCostManager<TCostLineItem>.CreateDataCostCellDetail(costLine.GroupId,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.ComboBox,
                            GridViewCellValueType.Integer);

                        costCellDetail.DataSource = XpressTestingFakeData.GroupDataSource;

                        return costCellDetail;
                    }
                case CostColumnContainer.CostColumn_Subgroup:
                    {
                        costCellDetail =
                            BaseCostManager<TCostLineItem>.CreateDataCostCellDetail(costLine.SubgroupId,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.ComboBox,
                            GridViewCellValueType.Integer);

                        List<string[]> subgroupArray = new List<string[]>();
                        if (costLine.Group != null &&
                            costLine.Group.Subgroups != null)
                        {
                            foreach (Subgroup subgroup in costLine.Group.Subgroups)
                            {
                                subgroupArray.Add(new string[] { subgroup.Id.ToString(), subgroup.Name });
                            }
                        }

                        costCellDetail.DataSource = subgroupArray.ToArray();

                        return costCellDetail;
                    }
                case CostColumnContainer.CostColumn_HPOR3rd:
                    {
                        costCellDetail =
                            BaseCostManager<TCostLineItem>.CreateDataCostCellDetail(costLine.ProductionSource,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.ComboBox,
                            GridViewCellValueType.String);

                        costCellDetail.DataSource = EntityConstants.ProductionSources;

                        return costCellDetail;
                    }
                case CostColumnContainer.CostColumn_TotalCost:
                    {
                        costCellDetail =
                            BaseCostManager<TCostLineItem>.CreateDataCostCellDetail(costLine.TotalCost,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.EditBox,
                            GridViewCellValueType.Decimal);

                        costCellDetail.DisplayValue = costLine.TotalCost.ToString("0.00");

                        return costCellDetail;
                    }
                case CostColumnContainer.CostColumn_TotalPrice:
                    {
                        costCellDetail =
                            BaseCostManager<TCostLineItem>.CreateDataCostCellDetail(costLine.TotalPrice,
                            GridViewColumnAccess.Edit,
                            GridViewColumnType.EditBox,
                            GridViewCellValueType.Decimal);

                        costCellDetail.DisplayValue = costLine.TotalPrice.ToString("0.00");

                        return costCellDetail;
                    }
                default:
                    {
                        break;
                    }
            }

            return null;
        }

        public virtual bool UpdateCellValue(TCostLineItem costLine, string columnName, object newValue)
        {
            GridViewCostCellDetail costCellDetail = this.GetCellDetail(costLine, columnName);
            
            if (costCellDetail == null)
            {
                return false;
            }

            bool updated = DataGridViewCellHandler.UpdateNewValueIntoCellDatail(newValue, costCellDetail);

            if (updated)
            {
                return this.UpdateCellValue(costLine, columnName, costCellDetail);
            }
            else
            {
                return false;
            }
        }

        protected virtual bool UpdateCellValue(TCostLineItem costLine, string columnName, GridViewCostCellDetail costCellDetail) 
        {
            switch (columnName)
            {
                case CostColumnContainer.CostColumn_Group:
                    {
                        costLine.GroupId = costCellDetail.GetValue<int>();

                        return true;
                    }
                case CostColumnContainer.CostColumn_Subgroup:
                    {
                        costLine.SubgroupId = costCellDetail.GetValue<int>();

                        return true;
                    }
                case CostColumnContainer.CostColumn_HPOR3rd:
                    {
                        ProductionSource outputProductionSource;

                        bool parsedSuccessfully = Enum.TryParse<ProductionSource>(costCellDetail.GetValue<string>(), out outputProductionSource);

                        if (parsedSuccessfully)
                        {
                            costLine.ProductionSource = outputProductionSource;
                        }

                        return parsedSuccessfully;
                    }
                case CostColumnContainer.CostColumn_TotalCost:
                    {
                        costLine.TotalCost = costCellDetail.GetValue<decimal>();

                        return true;
                    }
                case CostColumnContainer.CostColumn_TotalPrice:
                    {
                        costLine.TotalPrice = costCellDetail.GetValue<decimal>();

                        return true;
                    }
            }

            return false;
        }

        #region Public virtual members

        public virtual TCostLineItem CreateNewCostLineItem()
        {
            TCostLineItem newCostLine = Activator.CreateInstance<TCostLineItem>(); //效率慢，建议用 Unity or NInject IOC.

            newCostLine.ProductionSource = ProductionSource.HP;
            newCostLine.LastModifiedDateTime = DateTime.UtcNow;

            return newCostLine;
        }

        public virtual TCostLineItem CopyCostLineItem(TCostLineItem costLine) 
        {
            TCostLineItem copyOfCostLine = ObjectMapper.CopyObject<TCostLineItem>(costLine);

            return copyOfCostLine;
        }

        #endregion

        #region Public Static Methods for Create Cost Cell Details

        public static GridViewCostCellDetail CreateDataCostCellDetail(GridViewCellValueType valueType)
        {
            GridViewCostCellDetail cellDetail = new GridViewCostCellDetail();
            cellDetail.ValueType = valueType;

            return cellDetail;
        }

        public static GridViewCostCellDetail CreateDataCostCellDetail(object value, 
                                                                      GridViewColumnAccess accessStatus,
                                                                      GridViewColumnType columnType,
                                                                      GridViewCellValueType valueType)
        {
            GridViewCostCellDetail cellDetail = new GridViewCostCellDetail();
            cellDetail.AccessStatus = accessStatus;
            cellDetail.ColumnType = columnType;
            cellDetail.ValueType = valueType;
            cellDetail.Value = value;

            return cellDetail;
        }

        #endregion


        #region ICostManager members 隐式 实现 接口 

        IEnumerable<CostLineItemBase> ICostManager.CurrentDisplayedCostLines
        {
            get 
            {
                return this.currentDisplayedCostLines;
            }
        }

        void ICostManager.InitializeAllCostLines()
        {
            this.InitializeAllCostLines();
        }

        GridViewCostCellDetail ICostManager.GetCellDetail(CostLineItemBase costLine, string columnName)
        {
            return this.GetCellDetail((TCostLineItem)costLine, columnName);
        }

        bool ICostManager.UpdateCellValue(CostLineItemBase costLine, string columnName, object newValue)
        {
            return this.UpdateCellValue((TCostLineItem)costLine, columnName, newValue);
        }

        void ICostManager.AddCostLine()
        {
            this.AddCostLine();
        }

        void ICostManager.CopyCostLine(CostLineItemBase costLine)
        {
            this.CopyCostLine((TCostLineItem)costLine);
        }

        void ICostManager.DeleteCostLine(int costLineId)
        {
            this.DeleteCostLine(costLineId);
        }

        void ICostManager.Save()
        {
            this.Save();
        }

        #endregion

    }
}
