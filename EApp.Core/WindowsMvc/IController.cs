using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.WindowsMvc
{
    public interface IController
    {
        IView View { get; set; }

        /// <summary>
        /// Execute Action.
        /// </summary>
        void Execute(string actionName, IDictionary<string, object> actionParameters);
    }
}
