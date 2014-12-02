using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.WindowsMvc
{
    public interface IView
    {
        /// <summary>
        /// Whether current action is thread asynchronous operation.
        /// </summary>
        bool IsAsync { get; }
    }
}
