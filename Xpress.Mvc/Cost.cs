using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EApp.Common.AsynComponent;
using EApp.Common.DataAccess;
using EApp.Core.Application;
using EApp.Core.Configuration;
using EApp.Windows.Mvc;
using Xpress.Mvc.Logic;
using Xpress.Mvc.Models;

namespace Xpress.Mvc
{
    public partial class Cost : FormViewBase
    {
        public Cost()
        {
            InitializeComponent();
        }

        public void BindCosts(CostModel costModel) 
        { 
            //Bind cost to data grid view.
            dataGridView1.DataSource = costModel.Costs;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.View.Action("AddCost");


            //Create Instance 1: DbGateway db = new DbGateway("MySql")

            //Create Instance 2: DbGateway.Default

            //Create Instance 3: DbGateway db = new DbGateway(DatabaseType.SqlServer, "server=localhost\OSPTTESTDEV;database=TESTDB;User ID=sa;Password=sa");

            // Actually, no need to create and open a connection.
            //DbConnection connection = DbGateway.Default.OpenConnectiion();

            // We can use BeginTransaction to first create and open a connection
            // and then create a transaction using the opened connection. we don't need to care the connection.

            DbConnection connection = DbGateway.Default.OpenConnectiion();

            DbTransaction trans = DbGateway.Default.BeginTransaction(connection);

            try
            {
                // Delete
                DbGateway.Default.Delete("user", 
                    "user_id > @user_id and user_email = @user_email",
                    new object[] { 1000, "airsoft_ft@126.com" }, trans);

                string userName;
                string email = "airsoft_ft@126.com";
                string password;

                string topicName;

                string topicDesc;

                // Insert
                for (int index = 0; index < 10; index++)
                {
                    userName = "Admin " + (index + 1).ToString();

                    password = "change_2014121" + index.ToString();

                    topicName = "Topic " + (index + 1).ToString();

                    topicDesc = "Description " + (index + 1).ToString();

                    DbGateway.Default.Insert("user",
                                              new object[] { userName, null, email, password }, 
                                              trans);

                    DbGateway.Default.Insert("topic",
                                              new object[] { topicName, topicDesc }, trans);

                    //DbGateway.Default.Insert("user",
                    //    new string[] { "user_name", "user_email", "user_password" },
                    //    new object[] { userName, email, password }, trans);
                }


                // Update
                DbGateway.Default.Update("user",
                                         new string[] { "user_nick_name" },
                                         new object[] { "卡洛斯" },
                                         "user_id=@id",
                                         new object[] { 1000 },
                                         trans);

                // customized Non Query SQL scripts.
                DbGateway.Default.ExecuteNonQuery("update [user] set [user_email]=@email where [user_id]=@id",
                                                  new object[] {"119075838@qq.com", 1000 }, 
                                                  trans);

                trans.Commit();                
            }
            catch (Exception ex)
            {
                trans.Rollback();
            }
            finally
            {
                DbGateway.Default.CloseConnection(connection);
            }

            // 注意，transaction 提交之后， transaction 的 connection 将变为null,
            // 所以 调用 transaction 的Commit 之后，不能再使用带 transaction参数的方法,
            // 应该直接调用无transaction 参数的方法。
            string userQuerySql = @"select [user_name], [user_email], [user_password] from [user] where [user_id]=@id";

            //读取后直接关闭connection. 这样也避免了死锁。
            DataSet ds = DbGateway.Default.ExecuteDataSet(userQuerySql, new object[] { 1000 });

            // 以下方法在 Commit 调用 之后 再执行 是错误的，如果需要在Commit 之后 调用 带transaction的方法那说明你的数据库操作逻辑本身就有问题
            // 你应该在Commit 之前获取你想要的数据，用ReadCommitted 的隔离级别。可研究 转账 业务,要保证是同一个事物里面，所以不管读取还是
            // 更新都应该在事物提交之前去做。提交之后读数据已经没必要在 用事物去读了，Query执行后直接关闭connecton.
            //DataSet ds = DbGateway.Default.ExecuteDataSet(userQuerySql, new object[] { 1000 }, trans);

            DataTable dt = ds.Tables[0];

            string userEmail = dt.Rows[0]["user_email"].ToString();

            using (IDataReader dr = DbGateway.Default.ExecuteReader(userQuerySql, new object[] { 1000 }))
            {
                dr.Read();

                string userPassword = dr["user_password"].ToString();

                //dr.Close();

                //dr.Dispose();
            }

        }

        private IAsyncTask exportTask;

        private void btnExport_Click(object sender, EventArgs e)
        {
            rbMessage.Clear();

            exportTask = new DataExporter();

            exportTask.RunAsync(@"c:\\TestDataFile.txt");

            exportTask.ProgressChanged += new ProgressChangedEventHandler(delegate(object o, ProgressChangedEventArgs args) 
                {
                    if (args != null &&
                        args.UserState != null)
                    {
                        this.rbMessage.AppendText(args.UserState.ToString());
                    }
                });

            exportTask.Completed += new AsyncCompletedEventHandler(delegate(object o, AsyncCompletedEventArgs args) 
                {
                    this.rbMessage.AppendText("Export Successfuly.");
                });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            exportTask.CancelAsync();
        }
    }
}
