using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Query;

namespace EApp.Data.Query.Criterias
{
    public abstract class OperatorSqlCriteria : ISqlCriteria
    {
        protected OperatorSqlCriteria() { }

        protected OperatorSqlCriteria(DbProvider dbProvider, string dbColumn)
        {
            this.DbProvider = dbProvider;
            this.DbColumn = dbColumn;
            this.BuildedDbColumn = this.DbProvider.BuildColumnName(this.DbColumn).Trim();
            string tempParameterColumn = ParameterColumnCache.Instance.GetParameterColumn(dbColumn);
            this.ParameterColumn = this.DbProvider.BuildParameterName(tempParameterColumn).Trim();
        }

        public DbProvider DbProvider 
        {
            get;
            set;
        }

        public string DbColumn 
        { 
            get; 
            set; 
        }

        public string BuildedDbColumn
        {
            get;
            set;
        }

        public string ParameterColumn
        {
            get;
            set;
        }

        public static OperatorSqlCriteria Create(DbProvider dbProvider, string dbColumn, Operator @operator) 
        {
            switch (@operator)
            { 
                case Operator.Equal:
                    return new EqualSqlCriteria(dbProvider, dbColumn);
                case Operator.NotEqual:
                    return new NotEqualSqlCriteria(dbProvider, dbColumn);
                case Operator.GreaterThan:
                    return new GreaterThanSqlCriteria(dbProvider, dbColumn);
                case Operator.GreaterThanEqual:
                    return new GreaterThanSqlCriteria(dbProvider, dbColumn);
                case Operator.LessThan:
                    return new LessThanSqlCriteria(dbProvider, dbColumn);
                case Operator.LessThanEqual:
                    return new LessThanEqualSqlCriteria(dbProvider, dbColumn);
                case Operator.Contains:
                case Operator.StartsWith:
                case Operator.EndsWith:
                    return new LikeSqlCriteria(dbProvider, dbColumn);
                //case Operator.StartsWith:
                //    return new StartsWithSqlCriteria(dbProvider, dbColumn);
                //case Operator.EndsWith:
                //    return new EndsWithSqlCriteria(dbProvider, dbColumn);
                case Operator.In:
                    return new InSqlCriteria(dbProvider, dbColumn);
                case Operator.NotIn:
                    return new NotInSqlCriteria(dbProvider, dbColumn);
                default:
                    return null;
            }
        }

        public virtual string GetSqlCriteria() 
        {
            return string.Format(@" {0} {1} {2} ", this.BuildedDbColumn,
                                                   this.GetOperatorChar(), 
                                                   this.ParameterColumn);
        }

        protected abstract string GetOperatorChar(); 
    }
}
