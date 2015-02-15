using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper.Mapping
{
    /// <summary>
    /// Represent the column is a primary key in a database table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class KeyAttribute : ColumnAttribute
    {
        public KeyAttribute() { }

        public KeyAttribute(string name) : base(name) { }

        /// <summary>
        /// Gets or sets whether a column contains values that the database auto-generates.
        /// </summary>
        public bool IsDbGenerated
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sequence name
        /// </summary>
        public string SequenceName 
        { 
            get; 
            set; 
        }
    }
}
