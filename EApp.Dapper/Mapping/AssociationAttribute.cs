using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper.Mapping
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class AssociationAttribute : MemberAttribute
    {
        /// <summary>
        /// Gets or sets the member as the foreign key in an association representing a database relationship.
        /// </summary>
        public bool IsForeignKey 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets one or more members of the target entity class as key values on the other side of the association.
        /// Default = Id of the related class.
        /// </summary>
        public string OtherKey
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets members of this entity class to represent the key values on this side of the association.
        /// Default = Id of the containing class.
        /// </summary>
        public string ThisKey
        {
            get;
            set;
        }
    }
}
