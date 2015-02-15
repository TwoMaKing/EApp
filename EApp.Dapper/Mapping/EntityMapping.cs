using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.Dapper.Mapping
{
    public class EntityMapping : IEntityMapping
    {
        private string tableName;
        private Type entityType;
        private IMemberMapping[] members;

        public EntityMapping(string tableName, Type entityType, IMemberMapping[] members)
        {
            this.tableName = tableName;
            this.entityType = entityType;
            this.members = members;
        }

        private void InitializeMemberMapping()
        {

        }

        public string TableName
        {
            get 
            {
                return this.tableName;
            }
        }

        public Type EntityType
        {
            get 
            {
                return this.entityType;
            }
        }

        public IMemberMapping[] Members
        {
            get 
            {
                return this.members;
            }
        }

        public IMemberMapping[] PrimaryKeys
        {
            get 
            {
                return null;
            }
        }

        public IMemberMapping Version
        {
            get { throw new NotImplementedException(); }
        }

        public IMemberMapping Get(string memberName)
        {
            throw new NotImplementedException();
        }

        public IMemberMapping Get(MemberInfo member)
        {
            throw new NotImplementedException();
        }

    }
}
