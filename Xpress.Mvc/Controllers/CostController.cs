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

            this.View.BindCosts(costModel);
        }

    }
}
