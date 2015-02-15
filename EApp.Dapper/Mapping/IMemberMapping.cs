using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.Dapper.Mapping
{
    /// <summary>
    /// Member meta data.
    /// </summary>
    public interface IMemberMapping
    {
        /// <summary>
        /// Gets the entity mapping that contains this member mapping.
        /// </summary>
        IEntityMapping Entity { get; }

        /// <summary>
        /// Gets the instance of MemberInfo for the member mapping.
        /// </summary>
        MemberInfo Member { get; }

        /// <summary>
        /// Gets the database field name of the column.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the type of the member.
        /// </summary>
        Type MemberType { get; }

        /// <summary>
        /// Gets whether the member is a column.
        /// </summary>
        bool IsColumn { get; }

        /// <summary>
        /// Gets the type of the database column.
        /// </summary>
        DBType DbType { get; }

        /// <summary>
        /// Gets the length of the database column.
        /// </summary>
        int? Length { get; }

        /// <summary>
        /// Gets whether a column can contain null values.
        /// </summary>
        bool IsNullable { get; }

        /// <summary>
        /// Gets whether a column contains values that the database auto-generates.
        /// </summary>
        bool IsDbGenerated { get; }

        /// <summary>
        /// Gets the sequence name
        /// </summary>
        string SequenceName { get; }
        
        /// <summary>
        /// Gets whether the column type of the member is a database timestamp or version number.
        /// </summary>
        bool IsVersion { get; }

        /// <summary>
        /// Gets how to approach the detection of optimistic concurrency conflicts.
        /// </summary>
        UpdateCheck UpdateCheck { get; }

        /// <summary>
        /// Gets whether this class member represents a column that is part or all of the primary key of the table.
        /// </summary>
        bool IsPrimaryKey { get; }

        /// <summary>
        /// Gets whether is unique.
        /// </summary>
        bool IsUnique { get; }

        /// <summary>
        /// Gets whether the Member has a relationship with the entity which contains this member
        /// </summary>
        bool IsRelationship { get; }

        /// <summary>
        /// Gets whether the association represents a many-to-one relationship. The false represents one-to many.
        /// </summary>
        bool IsManyToOne { get; }

        /// <summary>
        /// Gets whether the member is the foreign key in an association representing a database relationship.
        /// </summary>
        bool IsForeignKey { get; }

        /// <summary>
        ///  Gets the entity on the other side of the association.
        /// </summary>
        IEntityMapping RelatedEntity { get; }

        /// <summary>
        /// Gets members of this keys.
        /// </summary>
        IMemberMapping[] ThisKeyMembers { get; }

        /// <summary>
        /// Gets members of other keys.
        /// </summary>
        IMemberMapping[] OtherKeyMembers { get; }

    }
}
