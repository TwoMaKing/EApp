using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.Dapper.Mapping
{
    /// <summary>
    /// Entity meta data.
    /// </summary>
    public interface IEntityMapping
    {
        /// <summary>
        /// Gets table name
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// Gets the entity type
        /// </summary>
        Type EntityType { get; }

        /// <summary>
        /// Get the list of members in the entity type
        /// </summary>
        IMemberMapping[] Members { get; }

        /// <summary>
        /// Gets the list of members which represents primary keys in the entity type
        /// </summary>
        IMemberMapping[] PrimaryKeys { get; }

        /// <summary>
        /// Get the member which represents version used to approach the detection of optimistic concurrency conflicts in the entity type
        /// </summary>
        IMemberMapping Version { get; }

        /// <summary>
        /// Get the specified member mapping by the name of the instance of MemberInfo.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IMemberMapping Get(string memberName);

        /// <summary>
        /// Get the specified member mapping by the instance of MemberInfo
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IMemberMapping Get(MemberInfo member);
    }
}
