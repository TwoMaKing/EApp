using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace EApp.Core.Exceptions
{
    public class DatabaseException : DbException
    {
        /// <summary>
        /// Initializes a new instance of the DatabaseException class.
        /// </summary>
        public DatabaseException() : base() { }

        /// <summary>
        /// Initializes a new instance of the DatabaseException class with
        ///  the specified error message.
        /// </summary>
        public DatabaseException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the System.Data.Common.DbException class with
        /// the specified error message and a reference to the inner exception that is
        /// the cause of this exception.
        /// </summary>
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }

    }
}
