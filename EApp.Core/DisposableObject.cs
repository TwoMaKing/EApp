using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;

namespace EApp.Core
{
    /// <summary>
    /// Represents that the derived classes are disposable objects.
    /// </summary>
    [ComVisible(true)]
    public abstract class DisposableObject : CriticalFinalizerObject, IDisposable 
    {
        /// <summary>
        /// Finalizes the object.
        /// </summary>
        ~DisposableObject() 
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">Indicates whether the object should be disposed explicitly.</param>
        protected abstract void Dispose(bool disposing);

        #region IDisposable members

        /// <summary>
        ///  Performs application-defined tasks associated with freeing, releasing, or
        ///  resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
