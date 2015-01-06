using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core;
using EApp.Core.DomainDriven.Domain;

namespace EApp.Common.Query
{
    public static class QueryBuilderExtension
    {
        public static IQueryBuilder<TEntity> IfThenElse<TEntity>(this IQueryBuilder<TEntity> queryBuilder,
                                                                 Expression<Func<dynamic, bool>> test,
                                                                 Expression<Func<TEntity, bool>> ifTrue,
                                                                 Expression<Func<TEntity, bool>> ifFalse, 
                                                                 bool isOr = false)
                                                                 where TEntity : class, IEntity
        {
            Expression.IfThenElse(test, ifTrue, ifFalse);

            return queryBuilder;
        }

        public static IQueryBuilder<TEntity> IfThenElse<TEntity>(this IQueryBuilder<TEntity> queryBuilder,
                                                                 Expression<Func<dynamic, bool>> test,
                                                                 IQueryBuilder<TEntity> ifTrue,
                                                                 IQueryBuilder<TEntity> ifFalse,
                                                                 bool isOr = false)
                                                                 where TEntity : class, IEntity
        {

            Expression.IfThenElse(test, ifTrue.QueryPredicate, ifFalse.QueryPredicate);

            return queryBuilder;
        }

        public static IQuery<TEntity> IfThenElse<TEntity>(this IQueryBuilder<TEntity> queryBuilder,
                                                          Expression<Func<dynamic, bool>> test, 
                                                          Func<IQuery<TEntity>> ifTrue,
                                                          Func<IQuery<TEntity>> ifFalse)
                                                          where TEntity : class, IEntity
        {
            ConditionalExpression conditionalExpression = 
                Expression.IfThenElse(test, Expression.Constant(ifTrue), Expression.Constant(ifFalse));

            Func<IQuery<TEntity>> funcQuery = Expression.Lambda<Func<IQuery<TEntity>>>(conditionalExpression).Compile();

            return funcQuery();
        }

        public static IQuery<TEntity> Switch<TEntity, TSwitchValueType>(
            this IQueryBuilder<TEntity> queryBuilder,
            Func<TSwitchValueType> switchValue,
            IDictionary<TSwitchValueType, Expression<Func<TEntity, bool>>> switchCasesMappingTestValues) 
            where TEntity : class, IEntity
        {
            if (switchCasesMappingTestValues == null ||
                switchCasesMappingTestValues.Count.Equals(0))
            {
                return queryBuilder;
            }

            List<SwitchCase> switchCaseList = new List<SwitchCase>();

            foreach (KeyValuePair<TSwitchValueType, Expression<Func<TEntity, bool>>> switchCaseTestValuePair in switchCasesMappingTestValues)
            {
                if (switchCaseTestValuePair.Value == null)
                {
                    continue;
                }

                SwitchCase switchCaseExpression = Expression.SwitchCase(Expression.Constant(switchCaseTestValuePair.Value),
                                                                        Expression.Constant(switchCaseTestValuePair.Key));

                switchCaseList.Add(switchCaseExpression);
            }

            TSwitchValueType switchVariableValue = switchValue.Invoke();

            Type switchVariableValueType = switchVariableValue.GetType();

            ParameterExpression switchParameterExpression = Expression.Parameter(switchVariableValueType, "Switch");

            Expression<Func<TEntity, bool>> defaultValue = (t) => true;

            SwitchExpression switchExpression = Expression.Switch(switchParameterExpression, 
                                                                  Expression.Constant(defaultValue), 
                                                                  switchCaseList.ToArray());

            Expression<Func<TEntity, bool>> queryPredicate =
                 Expression.Lambda<Func<TSwitchValueType, Expression<Func<TEntity, bool>>>>
                 (switchExpression, switchParameterExpression).Compile()(switchVariableValue);

            queryBuilder.Filter(queryPredicate);

            return queryBuilder;
        }


        public static IQuery<TEntity> Switch<TEntity, TSwitchValueType>(
            this IQueryBuilder<TEntity> queryBuilder,
            Func<TSwitchValueType> switchValue,
            IDictionary<TSwitchValueType, IQueryBuilder<TEntity>> switchCasesMappingTestValues)
            where TEntity : class, IEntity
        {
            if (switchCasesMappingTestValues == null ||
                switchCasesMappingTestValues.Count.Equals(0))
            {
                return queryBuilder;
            }

            List<SwitchCase> switchCaseList = new List<SwitchCase>();

            foreach (KeyValuePair<TSwitchValueType, IQueryBuilder<TEntity>> switchCaseTestValuePair in switchCasesMappingTestValues)
            {
                if (switchCaseTestValuePair.Value == null ||
                    switchCaseTestValuePair.Value.QueryPredicate == null)
                {
                    continue;
                }

                SwitchCase switchCaseExpression = Expression.SwitchCase(Expression.Constant(switchCaseTestValuePair.Value.QueryPredicate),
                                                                        Expression.Constant(switchCaseTestValuePair.Key));

                switchCaseList.Add(switchCaseExpression);
            }

            TSwitchValueType switchVariableValue = switchValue.Invoke();

            Type switchVariableValueType = switchVariableValue.GetType();

            ParameterExpression switchParameterExpression = Expression.Parameter(switchVariableValueType, "Switch");

            Expression<Func<TEntity, bool>> defaultValue = (t) => true;

            SwitchExpression switchExpression = Expression.Switch(switchParameterExpression,
                                                                  Expression.Constant(defaultValue),
                                                                  switchCaseList.ToArray());

            Expression<Func<TEntity, bool>> queryPredicate =
                 Expression.Lambda<Func<TSwitchValueType, Expression<Func<TEntity, bool>>>>
                 (switchExpression, switchParameterExpression).Compile()(switchVariableValue);

            queryBuilder.Filter(queryPredicate);

            return queryBuilder;
        }

        public static IQueryBuilder<TEntity> Switch<TEntity, TSwitchValueType>(
                this IQueryBuilder<TEntity> queryBuilder,
                Func<TSwitchValueType> switchValue,
                IDictionary<TSwitchValueType, Expression<Func<TEntity, bool>>> switchCasesMappingTestValues,
                bool isOr = false)
                where TEntity : class, IEntity
        {
            if (switchCasesMappingTestValues == null ||
                switchCasesMappingTestValues.Count.Equals(0))
            {
                return queryBuilder;
            }

            List<SwitchCase> switchCaseList = new List<SwitchCase>();

            foreach (KeyValuePair<TSwitchValueType, Expression<Func<TEntity, bool>>> switchCaseTestValuePair in switchCasesMappingTestValues)
            {
                if (switchCaseTestValuePair.Value == null)
                {
                    continue;
                }

                SwitchCase switchCaseExpression = Expression.SwitchCase(Expression.Constant(switchCaseTestValuePair.Value),
                                                                        Expression.Constant(switchCaseTestValuePair.Key));

                switchCaseList.Add(switchCaseExpression);
            }

            TSwitchValueType switchVariableValue = switchValue.Invoke();

            Type switchVariableValueType = switchVariableValue.GetType();

            ParameterExpression switchParameterExpression = Expression.Parameter(switchVariableValueType, "Switch");

            Expression<Func<TEntity, bool>> defaultValue = (t) => true;

            SwitchExpression switchExpression = Expression.Switch(switchParameterExpression,
                                                                  Expression.Constant(defaultValue),
                                                                  switchCaseList.ToArray());

            Expression<Func<TEntity, bool>> queryPredicate =
                 Expression.Lambda<Func<TSwitchValueType, Expression<Func<TEntity, bool>>>>
                 (switchExpression, switchParameterExpression).Compile()(switchVariableValue);

            queryBuilder.Filter(queryPredicate, isOr);

            return queryBuilder;
        }


        public static IQueryBuilder<TEntity> Switch<TEntity, TSwitchValueType>(
                this IQueryBuilder<TEntity> queryBuilder,
                Func<TSwitchValueType> switchValue,
                IDictionary<TSwitchValueType, IQueryBuilder<TEntity>> switchCasesMappingTestValues, 
                bool isOr = false)
                where TEntity : class, IEntity
        {
            if (switchCasesMappingTestValues == null ||
                switchCasesMappingTestValues.Count.Equals(0))
            {
                return queryBuilder;
            }

            List<SwitchCase> switchCaseList = new List<SwitchCase>();

            foreach (KeyValuePair<TSwitchValueType, IQueryBuilder<TEntity>> switchCaseTestValuePair in switchCasesMappingTestValues)
            {
                if (switchCaseTestValuePair.Value == null ||
                    switchCaseTestValuePair.Value.QueryPredicate == null)
                {
                    continue;
                }

                SwitchCase switchCaseExpression = Expression.SwitchCase(Expression.Constant(switchCaseTestValuePair.Value.QueryPredicate),
                                                                        Expression.Constant(switchCaseTestValuePair.Key));

                switchCaseList.Add(switchCaseExpression);
            }

            TSwitchValueType switchVariableValue = switchValue.Invoke();

            Type switchVariableValueType = switchVariableValue.GetType();

            ParameterExpression switchParameterExpression = Expression.Parameter(switchVariableValueType, "Switch");

            Expression<Func<TEntity, bool>> defaultValue = (t) => true;

            SwitchExpression switchExpression = Expression.Switch(switchParameterExpression,
                                                                  Expression.Constant(defaultValue),
                                                                  switchCaseList.ToArray());

            Expression<Func<TEntity, bool>> queryPredicate =
                 Expression.Lambda<Func<TSwitchValueType, Expression<Func<TEntity, bool>>>>
                 (switchExpression, switchParameterExpression).Compile()(switchVariableValue);

            queryBuilder.Filter(queryPredicate, isOr);

            return queryBuilder;
        }


        public static IQuery<TEntity> SwitchOrderBy<TEntity, TSwitchValueType>(
                this IQueryBuilder<TEntity> queryBuilder,
                Func<TSwitchValueType> switchValue,
                IDictionary<TSwitchValueType, Expression<Func<TEntity, dynamic>>> switchCasesMappingTestValues)
                where TEntity : class, IEntity
        {
            if (switchCasesMappingTestValues == null ||
                switchCasesMappingTestValues.Count.Equals(0))
            {
                return queryBuilder;
            }

            List<SwitchCase> switchCaseList = new List<SwitchCase>();

            foreach (KeyValuePair<TSwitchValueType, Expression<Func<TEntity, dynamic>>> switchCaseTestValuePair in switchCasesMappingTestValues)
            {
                if (switchCaseTestValuePair.Value == null)
                {
                    continue;
                }

                SwitchCase switchCaseExpression = Expression.SwitchCase(Expression.Constant(switchCaseTestValuePair.Value),
                                                                        Expression.Constant(switchCaseTestValuePair.Key));

                switchCaseList.Add(switchCaseExpression);
            }

            TSwitchValueType switchVariableValue = switchValue.Invoke();

            Type switchVariableValueType = switchVariableValue.GetType();

            ParameterExpression switchParameterExpression = Expression.Parameter(switchVariableValueType, "Switch");

            Expression<Func<TEntity, dynamic>> defaultValue = (t) => t.Id;

            SwitchExpression switchExpression = Expression.Switch(switchParameterExpression,
                                                                  Expression.Constant(defaultValue),
                                                                  switchCaseList.ToArray());

            Expression<Func<TEntity, dynamic>> queryPredicate =
                 Expression.Lambda<Func<TSwitchValueType, Expression<Func<TEntity, dynamic>>>>
                 (switchExpression, switchParameterExpression).Compile()(switchVariableValue);

            queryBuilder.OrderBy(queryPredicate);

            return queryBuilder;
        }


    }
}
