using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Windows.Mvc
{
    
    public interface IViewAction
    {
        void Action(string actionName);

        void Action(string actionName, ICollection<object> actionParameters);

        void Action(string actionName, string controllerName);

        void Action(string actionName, string controllerName, ICollection<object> actionParameters);
    }
}
