using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Windows.Mvc
{
    public class AsynViewAction : IViewAction
    {
        public void Action(string actionName)
        {
            throw new NotImplementedException();
        }

        public void Action(string actionName, ICollection<object> actionParameters)
        {
            throw new NotImplementedException();
        }

        public void Action(string actionName, string controllerName)
        {
            throw new NotImplementedException();
        }

        public void Action(string actionName, string controllerName, ICollection<object> actionParameters)
        {
            throw new NotImplementedException();
        }
    }
}
