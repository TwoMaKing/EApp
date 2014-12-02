using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Windows.Mvc
{
    public class ActionExecutedContext
    {
        private ActionDescriptor actionDescriptor;

        public ActionExecutedContext() 
        { 
        
        }

        public ActionExecutedContext(ActionDescriptor actionDescriptor, bool canceled, Exception exception)
        {
            this.actionDescriptor = actionDescriptor;

            this.Canceled = canceled;

            this.Exception = exception;
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
        /// Gets or sets a value that indicates that this System.Web.Mvc.ActionExecutedContext
        /// object is canceled.
        /// </summary>
        public virtual bool Canceled { get; set; }

        /// <summary>
        /// Gets or sets the exception that occurred during the execution of the action
        /// method, if any.
        /// </summary>
        public virtual Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the exception is handled.
        /// </summary>
        public bool ExceptionHandled { get; set; }
        //
        // Summary:
        //     Gets or sets the result returned by the action method.
        //
        // Returns:
        //     The result returned by the action method.
        public object Result { get; set; }

    }
}
