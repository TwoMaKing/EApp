using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper.Mapping
{
    /// <summary>
    /// Designates a class as an entity class that is associated with a database table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited=false)]
    public class TableAttribute : Attribute
    {
        private string name;

        public TableAttribute(string name)
        {
            this.name = name;
        }

        public string Name 
        {
            get 
            {
                return this.name;
            }
            set 
            {
                this.name = value;
            }
        }
    }
}
