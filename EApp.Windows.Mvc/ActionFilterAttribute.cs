using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Windows.Mvc
{
    public abstract class ActionFilterAttribute : FilterAttribute, IActionFilter
    {
        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
            return;
        }

        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            return;
        }
    }
}
