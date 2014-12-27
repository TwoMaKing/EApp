using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using EApp.Common.AsynComponent;
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

}
