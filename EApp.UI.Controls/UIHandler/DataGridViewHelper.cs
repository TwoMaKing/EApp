using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.UI.Controls.GridView;
using EApp.Core.Application;

namespace EApp.UI.Controls.UIHandler
{
    public sealed class DataGridViewHelper
    {
        private DataGridViewHelper() { }

        public static void BindComboBoxCellDataSource(DataGridViewComboBoxCell cell, string[][] dataSource)
        {
            if (dataSource == null ||
                dataSource.Length.Equals(0))
            {
                cell.DataSource = null;
                return;
            }
            List<DataSourceNode> dataSourceNodeList = BindArrayDataHelper.GetDataSource(dataSource, 0, 1);

            cell.DataSource = dataSourceNodeList;
            cell.DisplayMember = "Text";
            cell.ValueMember = "Value";
        }

        public static void BindComboBoxCellDataSource(DataGridViewComboBoxColumn column, string[][] dataSource)
        {
            if (dataSource == null ||
                dataSource.Length.Equals(0))
                return;

            List<DataSourceNode> dataSourceNodeList = BindArrayDataHelper.GetDataSource(dataSource, 0, 1);

            column.DataSource = dataSourceNodeList;
            column.DisplayMember = "Text";
            column.ValueMember = "Value";
            column.SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        public static void BindComboBoxCellDataSource(DataGridViewComboBoxEditingControl control, string[][] dataSource)
        {
            if (dataSource == null ||
                dataSource.Length.Equals(0))
                return;

            List<DataSourceNode> dataSourceNodeList = BindArrayDataHelper.GetDataSource(dataSource, 0, 1);

            control.DataSource = dataSourceNodeList;
            control.DisplayMember = "Text";
            control.ValueMember = "Value";
        }

        public static void SetDataGridViewCellImage(DataGridViewImageCell imageCell, ValueImage valueImage)
        {
            if (!EAppRuntime.Instance.App.ResourceManagers.ContainsKey(valueImage.ResourceManagerName))
            {
                return;
            }

            imageCell.Value = EAppRuntime.Instance.App.ResourceManagers[valueImage.ResourceManagerName].GetImage(valueImage.ImageName);
        }

    }
}
