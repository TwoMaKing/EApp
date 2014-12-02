using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Windows.Mvc
{
    /// <summary>
    ///    Provides the context for the ActionExecuting method of the System.Web.Mvc.ActionFilterAttribute
    ///    class.
    /// </summary>
    public class ActionExecutingContext
    {
        private ActionDescriptor actionDescriptor;

        public ActionExecutingContext() { }

        /// <summary>
        /// Initializes a new instance of the System.Web.Mvc.ActionExecutingContext class
        ///  by using the specified controller context, action descriptor, and action-method
        ///  parameters.
        /// </summary>
        public ActionExecutingContext(ActionDescriptor actionDescriptor, IDictionary<string, object> actionParameters) 
        {
            this.actionDescriptor = actionDescriptor;

            this.ActionParameters = actionParameters;
        }

        /// <summary>
        /// Gets or sets the action descriptor.
        /// </summary>
        public ActionDescriptor ActionDescriptor 
        {
            get 
            {
                return this.actionDescriptor;
            }
            set
            {
                this.actionDescriptor = value;
            }
        }

        /// <summary>
        /// Gets or sets the action-method parameters.
        /// </summary>
        public virtual IDictionary<string, object> ActionParameters { get; set; }

        /// <summary>
        /// Gets or sets the result that is returned by the action method.
        /// </summary>
        public object Result { get; set; }
    }
}
