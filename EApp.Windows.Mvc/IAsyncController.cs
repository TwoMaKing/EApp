using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.WindowsMvc;

namespace EApp.Windows.Mvc
{
    /// <summary>
    /// Defines the methods that are required for an asynchronous controller.
    /// </summary>
    public interface IAsyncController : IController
    {
        
        /// <summary>
        /// Executes the specified action.
        /// </summary>
        IAsyncResult BeginExecute(string actionName, IDictionary<string, object> actionParameters, AsyncCallback callback, object state);

        /// <summary>
        /// Ends the asynchronous operation.
        /// </summary>
        /// <param name="asyncResult"></param>
        void EndExecute(IAsyncResult asyncResult);
    }
}
