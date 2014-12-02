using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.UI.Controls.UIHandler
{
    #region Customed DataSource Node class

    public class DataSourceNode
    {
        private string text;
        private string value;

        public DataSourceNode(string value, string text)
        {
            this.value = value;

            this.text = text;
        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }
        }

        public override bool Equals(Object obj)
        {
            if (obj.GetType() != typeof(DataSourceNode))
            {
                return false;
            }
            if ((obj as DataSourceNode).Text.Equals(this.Text) && (obj as DataSourceNode).Value.Equals(this.Value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    #endregion

    /// <summary>
    /// Summary description for BindArrayDataLogic.
    /// </summary>
    public sealed class BindArrayDataHelper
    {
        private BindArrayDataHelper()
        {
            
        }

        /// <summary>
        /// Convert string[][] into an list used for data source.
        /// </summary>
        /// <returns></returns>
        public static List<DataSourceNode> GetDataSource(string[][] stringArray, int indexVal, int indexText)
        {
            List<DataSourceNode> dataSourceNodeList = new List<DataSourceNode>();

            for (int i = 0; i < stringArray.Length; i++)
            {
                dataSourceNodeList.Add(new DataSourceNode(stringArray[i][indexVal], stringArray[i][indexText]));
            }

            return dataSourceNodeList;
        }

        public static string[][] ConvertDataSourceNodesToStringArray(object dataSource)
        {
            if (dataSource == null)
                return null;

            if (!(dataSource is IEnumerable<DataSourceNode>))
                return null;

            IEnumerable<DataSourceNode> dataSourceNodes = (IEnumerable<DataSourceNode>)dataSource;

            return ConvertDataSourceNodesToStringArray(dataSourceNodes);
        }

        public static string[][] ConvertDataSourceNodesToStringArray(IEnumerable<DataSourceNode> dataSourceNodes)
        {
            if (dataSourceNodes == null)
                return null;

            List<string[]> stringArrayList = new List<string[]>();

            foreach (DataSourceNode node in dataSourceNodes )
            {
                stringArrayList.Add(new string[] { node.Value, node.Text });
            }

            return stringArrayList.ToArray();
        }
    }
}
