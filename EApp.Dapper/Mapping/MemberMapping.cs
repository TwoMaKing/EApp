using EApp.Common.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.Dapper.Mapping
{
    public class MemberMapping : IMemberMapping
    {
        private readonly char[] separators = new char[] { ' ', ',', '|' };

        private MemberInfo memberInfo;

        private MemberAttribute memberAttribute;

        private IEntityMapping entity;

        private bool isColumn;

        private bool isPrimaryKey;

        private ColumnAttribute columnAttribute;

        private KeyAttribute key;

        private string name;

        private Type memberType;

        private DBType dbType = DBType.Unkonw;

        private int? length;

        private bool isNullable = true;

        private bool isUnique;
        
        private bool isDbGenerated;

        private string sequenceName;

        private bool isVersion;

        private UpdateCheck updateCheck = UpdateCheck.Never;

        private bool isRelationship;

        private bool isManyToOne;

        private bool isForeignKey;

        private Type relatedType;

        private IEntityMapping relatedEntity;

        private IMemberMapping[] thisKeyMembers;

        private IMemberMapping[] otherKeyMembers;

        public MemberMapping(MemberInfo memberInfo,  MemberAttribute memberAttribute, IEntityMapping entityMapping)
        {
            this.entity = entityMapping;

            this.memberInfo = memberInfo;

            this.memberAttribute = memberAttribute;

            this.memberType = memberInfo.GetMemberType();

            if (memberAttribute is ColumnAttribute)
            {
                this.InitializeColumnAttributeMapping((ColumnAttribute)memberAttribute);
            }
            else if (memberAttribute is AssociationAttribute)
            {
                var isEnumerableType = memberType != typeof(string) &&
                                       memberType != typeof(byte[]) &&
                                       typeof(IEnumerable).IsAssignableFrom(memberType);

                this.isRelationship = true;

                this.InitializeAssociationAttributeMapping((AssociationAttribute)memberAttribute, isEnumerableType);
            }
        }

        private void InitializeColumnAttributeMapping(ColumnAttribute column)
        {
            this.name = string.IsNullOrEmpty(column.Name) ? this.memberInfo.Name : column.Name;

            if (column is KeyAttribute)
            {
                this.isPrimaryKey = true;

                this.key = (KeyAttribute)this.memberAttribute;

                this.isDbGenerated = this.isPrimaryKey && this.key.IsDbGenerated;

                this.sequenceName = this.key.SequenceName;
            }

            this.memberType = this.memberInfo.ReflectedType;
            this.dbType = column.DbType;

            if (this.dbType == DBType.Char ||
                this.dbType == DBType.NChar ||
                this.dbType == DBType.NVarChar ||
                this.dbType == DBType.VarChar)
            {
                this.length = column.Length;
            }

            this.isNullable = this.isPrimaryKey ? false : column.IsNullable;
            this.isUnique = this.isPrimaryKey ? true : column.IsUnique;

            if (column.IsVersion &&
               (this.dbType == DBType.Int16 ||
                this.dbType == DBType.Int32 ||
                this.dbType == DBType.Int64 ||
                this.dbType == DBType.DateTime))
            {
                this.isVersion = true;
            }
            else
            {
                throw new MappingException(string.Format("Invalid Version member type '{0}' for  '{1}' , version type must be int or datetime type.", 
                                           this.dbType.ToString(),
                                           this.entity.EntityType.Name + "." + this.memberInfo.Name));
            }

            this.updateCheck = column.UpdateCheck;
        }

        private void InitializeAssociationAttributeMapping(AssociationAttribute association, bool isEnumerableType)
        {
            this.isForeignKey = association.IsForeignKey;

            if (this.isForeignKey || 
                association is ManyToOneAttribute)
            {
                this.isManyToOne = true;
            }

            if (isEnumerableType)
            {
                this.relatedType = ReflectionHelper.GetElementType(this.MemberType);
            }
            else
            {
                this.relatedType = this.memberType;
            }

            var tableAttribute = this.relatedType.GetCustomAttributes<TableAttribute>().FirstOrDefault();

            string thisKey = association.ThisKey;
            string otherKey = association.OtherKey;

            this.relatedEntity = new EntityMapping(tableAttribute.Name, this.relatedType, null);

            this.thisKeyMembers = thisKey.Split(separators).Select(m => GetMemberMapping(this.Entity.EntityType, m, this.entity)).ToArray();

            this.otherKeyMembers = this.relatedEntity.PrimaryKeys;
        }

        private IMemberMapping GetMemberMapping(Type type, string memberName, IEntityMapping entity)
        {

            MemberInfo member = type.GetMember(memberName).FirstOrDefault();

            ColumnAttribute column = member.GetCustomAttributes<ColumnAttribute>().FirstOrDefault();

            return new MemberMapping(member, column, entity);
        }

        public IEntityMapping Entity
        {
            get
            {
                return this.entity;
            }
        }

        public MemberInfo Member
        {
            get 
            {
                return this.memberInfo;
            }
        }

        public string Name
        {
            get 
            { 
                return this.name;
            }
        }

        public bool IsColumn
        {
            get 
            { 
                return this.isColumn; 
            }
        }

        public Type MemberType
        {
            get 
            {
                return this.memberType;
            }
        }

        public DBType DbType
        {
            get 
            {
                return this.dbType;
            }
        }

        public int? Length
        {
            get 
            {
                return this.length;
            }
        }

        public bool IsNullable
        {
            get 
            {
                return this.isNullable;
            }
        }

        public bool IsDbGenerated
        {
            get 
            {
                return this.isDbGenerated;
            }
        }

        public string SequenceName
        {
            get 
            {
                return this.sequenceName;
            }
        }

        public bool IsVersion
        {
            get 
            {
                return this.isVersion;
            }
        }

        public UpdateCheck UpdateCheck
        {
            get 
            {
                return this.updateCheck;
            }
        }

        public bool IsPrimaryKey
        {
            get 
            {
                return this.isPrimaryKey;
            }
        }

        public bool IsUnique
        {
            get 
            {
                return this.isUnique;
            }
        }

        public bool IsRelationship
        {
            get 
            {
                return this.isRelationship;
            }
        }

        public bool IsManyToOne
        {
            get 
            {
                return this.isManyToOne;
            }
        }

        public bool IsForeignKey
        {
            get 
            {
                return this.isForeignKey;
            }
        }

        public IEntityMapping RelatedEntity
        {
            get 
            {
                return this.relatedEntity;
            }
        }

        public IMemberMapping[] ThisKeyMembers
        {
            get 
            {
                return this.thisKeyMembers;
            }
        }

        public IMemberMapping[] OtherKeyMembers
        {
            get 
            {
                return this.otherKeyMembers;
            }
        }

    }
}
