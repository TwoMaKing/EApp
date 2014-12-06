using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Common.DataAccess;
using EApp.Core.Application;
using EApp.Core.Configuration;
using EApp.Windows.Mvc;
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


            //DbGateway db = new DbGateway("MySql")
            //DbGateway.Default

            //DbGateway db = new DbGateway(DatabaseType.SqlServer, "server=localhost\OSPTTESTDEV;database=TESTDB;User ID=sa;Password=sa");

            // Actually, no need to create and open a connection.
            //DbConnection connection = DbGateway.Default.OpenConnectiion();

            // We can use BeginTransaction to first create and open a connection
            // and then create a transaction using the opened connection. we don't need to care the connection.

            DbTransaction trans = DbGateway.Default.BeginTransaction();

            DbConnection conn = trans.Connection;

            try
            {
                // Delete
                DbGateway.Default.Delete("user", 
                    "user_id > @user_id and user_email = @user_email",
                    new object[] { 1000, "airsoft_ft@126.com" }, trans);

                string userName;
                string email = "airsoft_ft@126.com";
                string password;

                // Insert
                for (int index = 0; index < 2; index++)
                {
                    userName = "Admin " + (index + 1).ToString();

                    password = "change_2014121" + index.ToString();

                    DbGateway.Default.Insert("user",
                        new object[] { userName, null, email, password }, trans);

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

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
            }
            finally
            {
                DbGateway.Default.CloseConnection(conn);
            }

        }
    }
}
