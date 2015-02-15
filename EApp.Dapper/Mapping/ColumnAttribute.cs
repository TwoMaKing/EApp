using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper.Mapping
{
    /// <summary>
    /// Associates a class with a column in a database table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : MemberAttribute
    {
        public ColumnAttribute() : this(string.Empty)
        {
        }

        public ColumnAttribute(string name)
        {
            this.Name = name;
            this.DbType = DBType.Unkonw;
            this.IsNullable = true;
        }

        /// <summary>
        /// Gets or sets the type of the database column.
        /// </summary>
        public DBType DbType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the length of the database column.
        /// </summary>
        public int Length 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets whether a column can contain null values.
        /// </summary>
        public bool IsNullable
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets whether a column is unique.
        /// </summary>
        public bool IsUnique
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the column type of the member is a database timestamp or version number.
        /// </summary>
        public bool IsVersion 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets how to approach the detection of optimistic concurrency conflicts.
        /// </summary>
        public UpdateCheck UpdateCheck
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the precision for decimal. e.g. 1234.56 precision is 6.
        /// </summary>
        public byte Precision 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the scale for decimal. e.g. 1234.56 scale is 2.
        /// </summary>
        public byte Scale 
        { 
            get; 
            set; 
        }
    }
}
