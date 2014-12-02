using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Windows.Mvc
{
    public interface IActionFilter
    {
        // Summary:
        //     Called after the action method executes.
        //
        // Parameters:
        //   filterContext:
        //     The filter context.
        void OnActionExecuted(ActionExecutedContext filterContext);
        //
        // Summary:
        //     Called before an action method executes.
        //
        // Parameters:
        //   filterContext:
        //     The filter context.
        void OnActionExecuting(ActionExecutingContext filterContext);
    }
}
