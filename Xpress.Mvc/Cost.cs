using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Dynamic;
using System.Linq;
//using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Dappers;
using EApp.Common.AsynComponent;
using EApp.Common.Lambda;
using EApp.Common.Query;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Configuration;
using EApp.Core.Query;
using EApp.Data;
using EApp.Windows.Mvc;
using Microsoft.CSharp.RuntimeBinder;
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
            List<CostLine> costLines = new List<CostLine>();
            for (int i = 0; i < 10; i++) 
            {
                CostLine line = new CostLine();
                line.Id = i + 1000;
                line.Name = "Cost " + i.ToString();
                Random random = new Random(i);
                line.Price = random.Next();
                Random random1 = new Random(i);
                line.Popularity = random.Next();
                line.Type = (i % 2 == 0) ? "Annual" : "Non-Annual";

                costLines.Add(line);
            }

            //var o = GetOrderByQueryRequest(new OrderQueryRequest() { Id = 1000, HostInfo = "Test" })

            //this.View.Action("AddCost");


            //Create Instance 1: DbGateway db = new DbGateway("MySql")

            //Create Instance 2: DbGateway.Default

            //Create Instance 3: DbGateway db = new DbGateway(DatabaseType.SqlServer, "server=localhost\OSPTTESTDEV;database=TESTDB;User ID=sa;Password=sa");

            // Actually, no need to create and open a connection.
            //DbConnection connection = DbGateway.Default.OpenConnectiion();

            // We can use BeginTransaction to first create and open a connection
            // and then create a transaction using the opened connection. we don't need to care the connection.

            DbConnection connection = DbGateway.Default.OpenConnection();

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

                throw ex;
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

                if (!dr.IsClosed)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }

            IDataReader dr1 = DbGateway.Default.SelectReader("user",
                                                             new string[] { "user_name", "user_email", "user_password" },
                                                             "user_id > @id",
                                                             new object[] { 1 },
                                                             "user_name desc");

            while (dr1.Read())
            {
                string userPassword = dr1["user_password"].ToString();
            }

            if (!dr1.IsClosed)
            {
                dr1.Close();
                dr1.Dispose();
            }

            DataSet ds1 = DbGateway.Default.SelectDataSet("user",
                                                           new string[] { "user_name", "user_email", "user_password" },
                                                           "user_id > @id",
                                                           new object[] { 1 },
                                                           "user_name desc");

            string passwordString = ds1.Tables[0].Rows[1]["user_password"].ToString();

        }

        private Order GetOrderByQueryRequest(OrderQueryRequest queryRequest)
        {
            return null;

            //dynamic orderQueryRequest = new ExpandoObject();

            //// ExpandoObject implements the IDictionary<string, object> interface so it can be casted to IDictionary<string, object>
            //IDictionary<string, object> memberDictionary = (IDictionary<string, object>)orderQueryRequest;

            //System.Reflection.PropertyInfo[] properties = typeof(OrderQueryRequest).GetProperties(System.Reflection.BindingFlags.Public | 
            //                                        System.Reflection.BindingFlags.Instance);

            //foreach (System.Reflection.PropertyInfo propertyItem in properties)
            //{
            //    memberDictionary.Add(propertyItem.Name, propertyItem.GetValue(queryRequest, null));
            //}

            //var id = orderQueryRequest.Id;

            //var hostInfo = orderQueryRequest.HostInfo;


            //System.Linq.Dynamic.DynamicProperty p = new System.Linq.Dynamic.DynamicProperty("Id", typeof(int));

            //Type orderQueryRequestType = System.Linq.Dynamic.DynamicExpression.CreateClass(p);

            ////object o = Activator.CreateInstance(orderQueryRequestType);

            //NewExpression instance = Expression.New(orderQueryRequestType);

            //Expression pe = Expression.Call(instance, orderQueryRequestType.GetProperty("Id").GetSetMethod(), Expression.Constant(1000));

            //LabelTarget lt = Expression.Label(orderQueryRequestType, "ret");

            //Expression r = Expression.Return(lt, instance, orderQueryRequestType);

            //Expression.Lambda(e).Compile().DynamicInvoke();
            
            //Func<object> de = Expression.Lambda<Func<object>>(r).Compile();

            //object v = de();
            
            //System.Linq.Dynamic.DynamicExpression.Parse(typeof(int), Expression.Equal(

            //return null;

            //ConstructorInfo ci = typeof(ExpandoObject).GetConstructor(Type.EmptyTypes);

            //PropertyInfo pid = typeof(CostLine).GetProperty("Id");

            //NewExpression instance = DynamicExpression.New(ci);

            //Binder

            //CallSiteBinder idBinder = Binder.SetMember(CSharpBinderFlags.None, "Id", typeof(ExpandoObject),
            //    new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) });

            //DynamicExpression de = DynamicExpression.MakeDynamic(typeof(Func<ExpandoObject>), idBinder);

            //DynamicExpression.Lambda<Func<CallSiteBinder, ExpandoObject>>(de).Compile()(idBinder);

            //MemberExpression m = DynamicExpression.Property(instance, "Id");

            //DynamicExpression.Equal(m, DynamicExpression.Constant(id));

            //var o = Expression.Lambda<Func<dynamic>>(instance).Compile()();

            //return null;
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            List<Product> productList = new List<Product>();
            for (int i = 0; i < 10; i++)
            {
                Product product = new Product();
                product.Id = i + 1000;
                product.Name = "Cost " + i.ToString();
                Random random = new Random(i);

                product.Quotation = new Quotation();
                product.Quotation.Info = new Information();
                product.Quotation.StartingPrice = 1000 + i;
                product.Quotation.Info.Number = product.Id;

                product.AnnualQuotation = new AnnualQuotation();
                product.AnnualQuotation.StartingPrice = 5000 + i;

                product.RecommandLevel = i;
                product.Type = (i % 2 == 0) ? productType.Annual :
                                              productType.NonAnnual;

                productList.Add(product);
            }

            ProductRequest request = new ProductRequest();
            request.IsAnnualCover = true;
            request.OrderType = 1;

            IQueryBuilder<Product> qb = new QueryBuilder<Product>();

            var annualPriceOrderPredicateCases = new Dictionary<int, Expression<Func<Product, dynamic>>>();
            var annualPriceSortOrderCases = new Dictionary<int, SortOrder>();

            annualPriceOrderPredicateCases.Add(0, (p)=>p.AnnualQuotation != null ? p.AnnualQuotation.StartingPrice : p.Id);
            annualPriceOrderPredicateCases.Add(1, (p)=>p.AnnualQuotation != null ? p.AnnualQuotation.StartingPrice : p.Id);
            annualPriceOrderPredicateCases.Add(2, (p)=>p.RecommandLevel);

            annualPriceSortOrderCases.Add(0, SortOrder.Ascending);
            annualPriceSortOrderCases.Add(1, SortOrder.Descending);
            annualPriceSortOrderCases.Add(2, SortOrder.Descending);

            //qb.SwitchOrderBy(() => request.OrderType, annualPriceOrderPredicateCases, annualPriceSortOrderCases);

            var nonAnnualOrderPredicateCases = new Dictionary<int, Expression<Func<Product, dynamic>>>();
            var nonAnnualPriceSortOrderCases = new Dictionary<int, SortOrder>();

            nonAnnualOrderPredicateCases.Add(1, p => p.Quotation != null ? p.Quotation.StartingPrice : p.Id);
            nonAnnualOrderPredicateCases.Add(2, p => p.Quotation != null ? p.Quotation.StartingPrice : p.Id);
            nonAnnualOrderPredicateCases.Add(3, p => p.RecommandLevel);

            nonAnnualPriceSortOrderCases.Add(0, SortOrder.Ascending);
            nonAnnualPriceSortOrderCases.Add(1, SortOrder.Descending);
            nonAnnualPriceSortOrderCases.Add(2, SortOrder.Descending);

            //qb.SwitchOrderBy(() => request.OrderType, nonAnnualOrderPredicateCases, nonAnnualPriceSortOrderCases);

            IList<Product> pListOrdered = 
            qb.IfThenElse(() => request.IsAnnualCover,
                          () => qb.SwitchOrderBy(() => request.OrderType, annualPriceOrderPredicateCases, annualPriceSortOrderCases),
                          () => qb.SwitchOrderBy(() => request.OrderType, nonAnnualOrderPredicateCases, nonAnnualPriceSortOrderCases))
                          .ToList(productList);

            var annualPriceCases = new Dictionary<int, Expression<Func<Product, bool>>>();

            annualPriceCases.Add(1, p => p.Quotation.StartingPrice > 1003);

            annualPriceCases.Add(2, p => p.Quotation.StartingPrice > 1004);

            IList<Product> pList1 =  qb.Switch(()=>request.OrderType, annualPriceCases).ToList(productList);

            IList<Product> pList2 = qb.Filter(p => p.Id > 1000).
                                   And(p => p.Quotation.StartingPrice > 1002).
                                   OrderBy(p=>p.AnnualQuotation.StartingPrice).
                                   ToList(productList.AsQueryable());
            
        }
    }
}
