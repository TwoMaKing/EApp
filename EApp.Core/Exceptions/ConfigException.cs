using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EApp.Core.Exceptions
{
    /// <summary>
    /// Represents errors that occur when configuring EApp framework.
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(_Exception))]
    public class ConfigException : InfrastructureException
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <c>ConfigException</c> class.
        /// </summary>
        public ConfigException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <c>ConfigException</c> class with the specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConfigException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <c>ConfigException</c> class with the specified
        /// error message and the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The inner exception that is the cause of this exception.</param>
        public ConfigException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// Initializes a new instance of the <c>ConfigException</c> class with the specified
        /// string formatter and the arguments that are used for formatting the message which
        /// describes the error.
        /// </summary>
        /// <param name="format">The string formatter which is used for formatting the error message.</param>
        /// <param name="args">The arguments that are used by the formatter to build the error message.</param>
        public ConfigException(string format, params object[] args) : base(string.Format(format, args)) { }
        #endregion
    }
}
