using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Exceptions
{
    public class LoadAssemblyException : EAppException 
    { 
        /// <summary>
        /// Initializes a new instance of the LoadAssemblyException class.
        /// </summary>
        public LoadAssemblyException() : base() { }
        /// <summary>
        /// Initializes a new instance of the LoadAssemblyException class with the specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LoadAssemblyException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the LoadAssemblyException class with the specified
        /// error message and the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The inner exception that is the cause of this exception.</param>
        public LoadAssemblyException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// Initializes a new instance of the LoadAssemblyException class with the specified
        /// string formatter and the arguments that are used for formatting the message which
        /// describes the error.
        /// </summary>
        /// <param name="format">The string formatter which is used for formatting the error message.</param>
        /// <param name="args">The arguments that are used by the formatter to build the error message.</param>
        public LoadAssemblyException(string format, params object[] args) : base(string.Format(format, args)) { }
    }
}
