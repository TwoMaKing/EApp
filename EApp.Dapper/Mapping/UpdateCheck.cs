using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper.Mapping
{
    /// <summary>
    /// Specifies when objects are to be tested for concurrency conflicts.
    /// </summary>
    public enum UpdateCheck 
    {
        /// <summary>
        /// Always check. This is the default unless EApp.Dapper.Mapping.ColumnAttribute.IsVersion is true for a member.
        /// </summary>
        Always,

        /// <summary>
        /// Never check.
        /// </summary>
        Never,

        /// <summary>
        /// Check only when the object has been changed.
        /// </summary>
        WhenChanged
    }
}
