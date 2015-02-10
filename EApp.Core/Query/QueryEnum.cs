using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EApp.Core.Query
{
    [DataContract()]
    public enum Operator
    {
        Equal,

        NotEqual,

        GreaterThan,

        GreaterThanEqual,

        LessThan,

        LessThanEqual,

        Contains,

        StartsWith,

        EndsWith,

        In,

        NotIn
    }


    /// <summary>
    /// Specifies how items in a list are sorted.
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// The items are not sorted
        /// </summary>
        None = 0,

        /// <summary>
        /// The items are sorted in ascending order.
        /// </summary>
        Ascending = 1,

        /// <summary>
        /// The items are sorted in descending order.
        /// </summary>
        Descending = 2,
    }

}
