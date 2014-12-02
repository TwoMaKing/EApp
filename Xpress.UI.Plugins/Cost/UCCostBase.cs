using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Common.Util;
using EApp.Core.Application;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;
using EApp.Plugin.Generic.RibbonStyle;
using EApp.UI.Controls.GridView;
using EApp.UI.Controls.UIHandler;
using Xpress.Core.Common;
using Xpress.Core.Entities;
using Xpress.Core.Logic;
using Xpress.Core.Plugin;

namespace Xpress.UI.Plugins.Cost
{
    public partial class UCCostBase<TCostLineItem> : UCRibbonPluginViewBase, IEditableView where TCostLineItem : CostLineItemBase
    {
        public UCCostBase()
        {
            InitializeComponent();
        }

        protected BaseCostManager<TCostLineItem> currentCostManager;

        public virtual CostLineType CostType 
        {
            get 
            {
                return CostLineType.NA;
            }        
        }

        protected List<TCostLineItem> CurrentDisplayedCostLines
        {
            get
            {
                return new List<TCostLineItem>(this.currentCostManager.CurrentDisplayedCostLines);
            }
        }

        #region IEditableView members

        public bool ViewChanged
        {
            get { return true; }
        }

        public void PopulateView()
        {
            return;
        }

        #endregion

        #region Protected Members
        
        protected override void RefreshViewCore()
        {
            this.currentCostManager = CostManagerFactory.GetCostManager<TCostLineItem>();

            this.currentCostManager.InitializeAllCostLines();

            this.InitializeCostColumns();

            this.InitializeCostDataGridView();
        }

        protected override void RegisterPluginControllersEvents()
        {
            this.PluginManager.PluginControllers["Add New Cost"].Loading +=
                new CancelEventHandler(this.OnCostLineAddNewLoading);

            this.PluginManager.PluginControllers["Add New Cost"].Loaded +=
                new EventHandler<PluginLoadedEventArgs>(this.OnCostLineAddNewLoaded);

            base.RegisterPluginControllersEvents();
        }

        protected override void UnregisterPluginControllersEvents()
        {
            this.PluginManager.PluginControllers["Add New Cost"].Loading -=
                new CancelEventHandler(this.OnCostLineAddNewLoading);

            this.PluginManager.PluginControllers["Add New Cost"].Loaded -=
                new EventHandler<PluginLoadedEventArgs>(this.OnCostLineAddNewLoaded);

            base.UnregisterPluginControllersEvents();
        }

        protected virtual void InitializeCostColumns() 
        { 
            
        }

        protected virtual void InitializeCostDataGridView() 
        {
            this.dgvCostLine.Rows.Clear();
            this.dgvCostLine.RowCount = this.currentCostManager.CurrentDisplayedCostLines.Count();
            this.dgvCostLine.ClearSelection();
        }

        protected virtual void BindDataGridViewCellData(GridViewCostCellDetail costCellDetail, DataGridViewCellValueEventArgs e)
        {
            //display cell value depend on the enum value of the property ValueType
            switch (costCellDetail.ValueType)
            {
                case GridViewCellValueType.Boolean:
                    e.Value = costCellDetail.GetValue<bool>();
                    break;
                case GridViewCellValueType.DateTime:
                    e.Value = costCellDetail.DisplayValue; // may be need to format
                    break;
                case GridViewCellValueType.Decimal:
                    e.Value = costCellDetail.DisplayValue; // may be need to format
                    break;
                case GridViewCellValueType.Integer:
                    this.SetDataGridViewCellValue(costCellDetail, e);
                    break;
                case GridViewCellValueType.Percentage:
                    e.Value = costCellDetail.DisplayValue;
                    break;
                case GridViewCellValueType.String:
                    this.SetDataGridViewCellValue(costCellDetail, e);
                    break;
                case GridViewCellValueType.Icon:

                    ValueImage valueImage = costCellDetail.GetValue<ValueImage>();

                    e.Value = XpressCommonHelper.GetResourceImage(valueImage);

                    break;
                default:
                    break;
            }
        }

        protected void SetDataGridViewCellValue(GridViewCostCellDetail costCellDetail, DataGridViewCellValueEventArgs e)
        {
            DataGridViewCell currentCell = this.dgvCostLine.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (!(currentCell is DataGridViewComboBoxCell))
            {
                e.Value = costCellDetail.DisplayValue;

                return;
            }

            DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)currentCell;

            string[][] previousStringArrayList = BindArrayDataHelper.ConvertDataSourceNodesToStringArray(comboBoxCell.DataSource);

            if (!CommonHelper.EqualsStringArray(previousStringArrayList, costCellDetail.DataSource))
            {
                DataGridViewHelper.BindComboBoxCellDataSource(comboBoxCell, costCellDetail.DataSource);
            }

            e.Value = costCellDetail.Value.ToString();
        }

        #endregion

        #region Data Grid View events

        protected virtual void dgvCostLine_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.RowIndex > this.CurrentDisplayedCostLines.Count - 1 ||
                e.ColumnIndex < 0 ||
                e.ColumnIndex > this.dgvCostLine.Columns.Count - 1)
            {
                return;
            }

            TCostLineItem costLineByRowIndex = this.CurrentDisplayedCostLines[e.RowIndex];

            string columnName = this.dgvCostLine.Columns[e.ColumnIndex].Name;

            GridViewCostCellDetail costCellDetail = this.currentCostManager.GetCellDetail(costLineByRowIndex, columnName);

            if (costCellDetail == null)
            {
                return;
            }

            BindDataGridViewCellData(costCellDetail, e);
        }

        protected virtual void dgvCostLine_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            TCostLineItem costLineByRowIndex = this.CurrentDisplayedCostLines[e.RowIndex];

            string columnName = this.dgvCostLine.Columns[e.ColumnIndex].Name;

            this.currentCostManager.UpdateCellValue(costLineByRowIndex, columnName, e.Value);
        }

        protected virtual void dgvCostLine_DataError(object sender, DataGridViewDataErrorEventArgs e) 
        {
            return;
        }

        #endregion

        #region Module plugin Controller events

        protected virtual void OnCostLineAddNewLoading(object sender, CancelEventArgs e)
        {
            this.PluginManager.PluginControllers["Add New Cost"]
                .ServiceProvider.AddService(typeof(CostLineType), this.CostType);

            e.Cancel = false;
        }

        protected virtual void OnCostLineAddNewLoaded(object sender, PluginLoadedEventArgs e)
        {
            InitializeCostDataGridView();
        }


        #endregion

    }
}
