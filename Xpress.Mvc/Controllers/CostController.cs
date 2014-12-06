using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Windows.Mvc;
using Xpress.Mvc;
using Xpress.Mvc.Controllers;
using Xpress.Mvc.Models;
using System.Text.RegularExpressions;

namespace Xpress.Mvc.Controllers
{
    public class CostController : ControllerBase<Cost>
    {
        public CostController() : base() { }
        
        public void AddCost() 
        { 
            CostModel costModel = new CostModel();
            
            List<CostLine> costLines = new List<CostLine>();
            
            for(int i = 0; i <5; i++)
            {
                CostLine costLine = new CostLine();

                costLines.Add(costLine);
            }

            costModel.Costs = costLines;

            string sqlScript = "update set id = @id, name = @name, total price = @totalprice where age = @age";

            string[] columnArray = DiscoverParams(sqlScript);

            this.View.BindCosts(costModel);
        }

        private const string Parameter_Prefix = "@";

        private string[] DiscoverParams(string sql)
        {
            if (sql == null)
            {
                return null;
            }

            Regex r = new Regex("\\" + Parameter_Prefix + @"([\w\d_]+)");

            MatchCollection ms = r.Matches(sql);

            if (ms.Count == 0)
            {
                return null;
            }

            string[] paramNames = new string[ms.Count];
            for (int i = 0; i < ms.Count; i++)
            {
                paramNames[i] = ms[i].Value;
            }

            return paramNames;
        }


    }
}
