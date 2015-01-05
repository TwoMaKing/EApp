using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.AsynComponent;
using EApp.Common.Query;
using EApp.Data;
using Xpress.Mvc.Models;

namespace Xpress.Mvc.Logic
{
    public class DataExporter : AsyncTask
    {
        private int exportStep = 0;

        public DataExporter() : base() { }

        protected override void DoWork(params object[] arguments)
        {
            string outputFileName = arguments[0].ToString();
            
            this.ReportProgress(exportStep++, "Begin to Export to " + outputFileName + Environment.NewLine);

            FileStream fs = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            this.ExportUsersToFile(fs);

            this.ExportTopicsToFile(fs);

            fs.Flush();

            fs.Close();

            fs.Dispose();
        }

        private void ExportUsersToFile(FileStream stream) 
        {
            string userQuerySql = @"select [user_name], [user_email], [user_password] from [user]";

            using (IDataReader reader = DbGateway.Default.ExecuteReader(userQuerySql)) 
            {
                string userName = string.Empty;
                string userEmail = string.Empty;
                string userPassword = string.Empty;

                string content = string.Empty;
                byte[] contentByte = null;

                while (reader.Read())
                {
                    if (this.CancellationPending)
                    {
                        if (!reader.IsClosed)
                        {
                            reader.Close();
                            reader.Dispose();
                        }

                        return;
                    }

                    userName = reader[0].ToString();

                    userEmail = reader[1].ToString();

                    userPassword = reader[2].ToString();

                    content = userName + "," + userName + "," + userPassword + Environment.NewLine;

                    contentByte = Encoding.Default.GetBytes(content);

                    stream.Write(contentByte, 0, contentByte.Length);

                    exportStep++;

                    this.ReportProgress(exportStep, "[User]: " + userName + Environment.NewLine);
                }
            }
        }

        private void ExportTopicsToFile(FileStream stream) 
        {
            string topicQuerySql = @"select [topic_name], [topic_desc] from [topic]";

            using (IDataReader reader = DbGateway.Default.ExecuteReader(topicQuerySql))
            {
                string topicName = string.Empty;
                string desc = string.Empty;

                string content = string.Empty;
                byte[] contentByte = null;

                while (reader.Read())
                {
                    if (this.CancellationPending)
                    {
                        if (!reader.IsClosed)
                        {
                            reader.Close();
                            reader.Dispose();
                        }
                        return;
                    }

                    topicName = reader[0].ToString();

                    desc = reader[1].ToString();

                    content = topicName + "," + desc + Environment.NewLine;

                    contentByte = Encoding.Default.GetBytes(content);

                    stream.Write(contentByte, 0, contentByte.Length);

                    exportStep++;

                    this.ReportProgress(exportStep, "[Topic]: " + topicName + Environment.NewLine);
                }
            }
        }
    }


    public interface ISortable<T>
    {
        Expression<Func<T, bool>> WherePredicate { get; }

        Expression<Func<T, dynamic>> SortPredicate { get; }

        SortOrder Order { get; }
    }

    /// <summary>
    /// 全年 价格 排序 
    /// </summary>
    public class AnnualPriceSortale : ISortable<CostLine>
    {
        private SortOrder order;

        public AnnualPriceSortale(SortOrder order)
        {
            this.order = order;
        }

        public Expression<Func<CostLine, bool>> WherePredicate
        {
            get { return c => c.Type.Equals("Annual"); }
        }

        public Expression<Func<CostLine, dynamic>> SortPredicate
        {
            get { return c => c.Price; }
        }

        public SortOrder Order
        {
            get { return this.order; }
        }
    }

    /// <summary>
    /// 非全年价格排序
    /// </summary>
    public class NonAnnualPriceSortale : ISortable<CostLine>
    {
        private SortOrder order;

        public NonAnnualPriceSortale(SortOrder order)
        {
            this.order = order;
        }

        public Expression<Func<CostLine, bool>> WherePredicate
        {
            get { return c => c.Type.Equals("Non-Annual"); }
        }

        public Expression<Func<CostLine, dynamic>> SortPredicate
        {
            get { return c => c.Price; }
        }

        public SortOrder Order
        {
            get { return this.order; }
        }
    }

    public class AnnualPopularitySortale : ISortable<CostLine>
    {
        private SortOrder order;

        private AnnualPopularitySortale(SortOrder order) 
        {
            this.order = order;
        }

        public Expression<Func<CostLine, bool>> WherePredicate
        {
            get { return c => c.Type.Equals("Annual"); }
        }

        public Expression<Func<CostLine, dynamic>> SortPredicate
        {
            get { return c => c.Popularity; }
        }

        public SortOrder Order
        {
            get { return this.order; }
        }
    }

    public class NonAnnualPopularitySortale : ISortable<CostLine>
    {
        private SortOrder order;

        private NonAnnualPopularitySortale(SortOrder order)
        {
            this.order = order;
        }

        public Expression<Func<CostLine, bool>> WherePredicate
        {
            get { return c => c.Type.Equals("Non-Annual"); }
        }

        public Expression<Func<CostLine, dynamic>> SortPredicate
        {
            get { return c => c.Popularity; }
        }

        public SortOrder Order
        {
            get { return this.order; }
        }
    }


}
