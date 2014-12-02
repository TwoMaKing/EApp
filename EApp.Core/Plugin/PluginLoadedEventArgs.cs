using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EApp.Core.Plugin
{
    /// <summary>
    /// 
    /// </summary>
    public class PluginLoadedEventArgs : EventArgs
    {
        private bool cancelled;

        private Exception error;

        private object actionResult;

        public PluginLoadedEventArgs(Exception error,
                                     bool cancelled,
                                     object actionResult)
        {
            this.cancelled = cancelled;
            this.error = error;
            this.actionResult = actionResult;
        }


        /// <summary>
        /// Gets a value indicating whether an asynchronous operation has been canceled.
        /// Return true if the background operation has been canceled; otherwise false. The
        //  default is false.
        /// </summary>
        public bool Cancelled 
        {
            get 
            {
                return this.cancelled;
            }
        }

        /// <summary>
        /// Gets a value indicating which error occurred during an asynchronous operation.
        /// </summary>
        public Exception Error 
        {
            get 
            {
                return this.error;
            } 
        }

        /// <summary>
        /// The result that the plugin action returns.
        /// </summary>
        public object ActionResult 
        {
            get 
            { 
                return this.actionResult; 
            } 
        }

    }
}
