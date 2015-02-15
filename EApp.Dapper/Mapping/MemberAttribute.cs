using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper.Mapping
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class MemberAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of a column.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a private storage field to hold the value from a column.
        /// </summary>
        public string Storage { get; set; }
    }
}
