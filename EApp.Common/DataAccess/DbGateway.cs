using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.DataAccess
{
    public enum DataBaseType
    {
        /// <summary>
        /// Common SqlServer, including SQL Server 7.X, 8.X and 9.X
        /// </summary>
        SqlServer = 0,

        /// <summary>
        /// Oracle
        /// </summary>
        Oracle = 1,

        /// <summary>
        /// MySql
        /// </summary>
        MySql = 2,

        /// <summary>
        /// 
        /// </summary>
        Other = 3
    }


    public sealed class DbGateway
    {
        private DbGateway() 
        { 
        
        }

        public static void Insert(string commandText) 
        { 
        
        }

        public static void Insert(string commandText, bool trans) 
        { 
        
        }

        public static void Update(string commandText) 
        { 
        
        }

        public static void Update(string commandText, bool trans) 
        { 
        
        }

        public static void Delete(string commandText)
        {

        }

        public static void Delete(string commandText, bool trans)
        {

        }

    }
}
