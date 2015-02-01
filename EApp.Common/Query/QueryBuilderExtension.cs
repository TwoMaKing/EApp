using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core;
using EApp.Core.Query;

namespace EApp.Common.Query
{
    public static class QueryBuilderExtension
    {
        public static IQueryBuilder<T> IfThenElse<T>(this IQueryBuilder<T> queryBuilder,
                                                     Expression<Func<dynamic, bool>> test,
                                                     Expression<Func<T, bool>> ifTrue,
                                                     Expression<Func<T, bool>> ifFalse, 
                                                     bool isOr = false)
                                                     where T : class
        {
            Expression.IfThenElse(test, ifTrue, ifFalse);

            return queryBuilder;
        }

        public static IQueryBuilder<T> IfThenElse<T>(this IQueryBuilder<T> queryBuilder,
                                                     Expression<Func<dynamic, bool>> test,
                                                     IQueryBuilder<T> ifTrue,
                                                     IQueryBuilder<T> ifFalse,
                                                     bool isOr = false)
                                                     where T : class
        {

            Expression.IfThenElse(test, ifTrue.QueryPredicate, ifFalse.QueryPredicate);

            return queryBuilder;
        }

        public static IQuery<T> IfThenElse<T>(this IQueryBuilder<T> queryBuilder,
                                              Expression<Func<bool>> test, 
                                              Func<IQuery<T>> ifTrue,
                                              Func<IQuery<T>> ifFalse)
                                              where T : class
        {
            ParameterExpression testParameter = Expression.Parameter(typeof(bool), "test");

            ParameterExpression variable = Expression.Variable(typeof(Func<IQuery<T>>), "query");

            LabelTarget labelTarget = Expression.Label(typeof(Func<IQuery<T>>));

            ConditionalExpression conditionalExpression =
                Expression.IfThenElse(testParameter,
                Expression.Assign(variable, Expression.Constant(ifTrue)),
                Expression.Assign(variable, Expression.Constant(ifFalse)));

            GotoExpression returnExpression = Expression.Return(labelTarget, variable);

            LabelExpression labelExpression = Expression.Label(labelTarget, variable);

            //生成BlockExpression

            BlockExpression blocks = Expression.Block(

                new ParameterExpression[] { variable },

                conditionalExpression,

                returnExpression,

                labelExpression);

            Expression<Func<bool, Func<IQuery<T>>>> funcQueryExpression =
                Expression.Lambda<Func<bool, Func<IQuery<T>>>>(blocks, testParameter);

            return funcQueryExpression.Compile()(test.Compile()())();
        }

        public static IQuery<T> Switch<T, TSwitchValueType>(
            this IQueryBuilder<T> queryBuilder,
            Func<TSwitchValueType> switchValue,
            IDictionary<TSwitchValueType, Expression<Func<T, bool>>> testValueCases) 
            where T : class
        {
            if (testValueCases == null ||
                testValueCases.Count.Equals(0))
            {
                return queryBuilder;
            }

            List<SwitchCase> switchCaseList = new List<SwitchCase>();

            foreach (KeyValuePair<TSwitchValueType, Expression<Func<T, bool>>> switchCaseTestValuePair in testValueCases)
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

            Expression<Func<T, bool>> defaultValue = (t) => true;

            SwitchExpression switchExpression = Expression.Switch(switchParameterExpression, 
                                                                  Expression.Constant(defaultValue), 
                                                                  switchCaseList.ToArray());

            Expression<Func<T, bool>> queryPredicate =
                 Expression.Lambda<Func<TSwitchValueType, Expression<Func<T, bool>>>>
                 (switchExpression, switchParameterExpression).Compile()(switchVariableValue);

            queryBuilder.Filter(queryPredicate);

            return queryBuilder;
        }

        public static IQuery<T> Switch<T, TSwitchValueType>(
            this IQueryBuilder<T> queryBuilder,
            Func<TSwitchValueType> switchValue,
            IDictionary<TSwitchValueType, IQueryBuilder<T>> testValueCases)
            where T : class
        {
            if (testValueCases == null ||
                testValueCases.Count.Equals(0))
            {
                return queryBuilder;
            }

            List<SwitchCase> switchCaseList = new List<SwitchCase>();

            foreach (KeyValuePair<TSwitchValueType, IQueryBuilder<T>> switchCaseTestValuePair in testValueCases)
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

            Expression<Func<T, bool>> defaultValue = (t) => true;

            SwitchExpression switchExpression = Expression.Switch(switchParameterExpression,
                                                                  Expression.Constant(defaultValue),
                                                                  switchCaseList.ToArray());

            Expression<Func<T, bool>> queryPredicate =
                 Expression.Lambda<Func<TSwitchValueType, Expression<Func<T, bool>>>>
                 (switchExpression, switchParameterExpression).Compile()(switchVariableValue);

            queryBuilder.Filter(queryPredicate);

            return queryBuilder;
        }

        public static IQueryBuilder<T> Switch<T, TSwitchValueType>(
                this IQueryBuilder<T> queryBuilder,
                Func<TSwitchValueType> switchValue,
                IDictionary<TSwitchValueType, Expression<Func<T, bool>>> testValueCases,
                bool isOr = false)
                where T : class
        {
            if (testValueCases == null ||
                testValueCases.Count.Equals(0))
            {
                return queryBuilder;
            }

            List<SwitchCase> switchCaseList = new List<SwitchCase>();

            foreach (KeyValuePair<TSwitchValueType, Expression<Func<T, bool>>> switchCaseTestValuePair in testValueCases)
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

            Expression<Func<T, bool>> defaultValue = (t) => true;

            SwitchExpression switchExpression = Expression.Switch(switchParameterExpression,
                                                                  Expression.Constant(defaultValue),
                                                                  switchCaseList.ToArray());

            Expression<Func<T, bool>> queryPredicate =
                 Expression.Lambda<Func<TSwitchValueType, Expression<Func<T, bool>>>>
                 (switchExpression, switchParameterExpression).Compile()(switchVariableValue);

            queryBuilder.Filter(queryPredicate, isOr);

            return queryBuilder;
        }

        public static IQueryBuilder<T> Switch<T, TSwitchValueType>(
                this IQueryBuilder<T> queryBuilder,
                Func<TSwitchValueType> switchValue,
                IDictionary<TSwitchValueType, IQueryBuilder<T>> testValueCases, 
                bool isOr = false)
                where T : class
        {
            if (testValueCases == null ||
                testValueCases.Count.Equals(0))
            {
                return queryBuilder;
            }

            List<SwitchCase> switchCaseList = new List<SwitchCase>();

            foreach (KeyValuePair<TSwitchValueType, IQueryBuilder<T>> switchCaseTestValuePair in testValueCases)
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

            Expression<Func<T, bool>> defaultValue = (t) => true;

            SwitchExpression switchExpression = Expression.Switch(switchParameterExpression,
                                                                  Expression.Constant(defaultValue),
                                                                  switchCaseList.ToArray());

            Expression<Func<T, bool>> queryPredicate =
                 Expression.Lambda<Func<TSwitchValueType, Expression<Func<T, bool>>>>
                 (switchExpression, switchParameterExpression).Compile()(switchVariableValue);

            queryBuilder.Filter(queryPredicate, isOr);

            return queryBuilder;
        }

        public static IQuery<T> SwitchOrderBy<T, TSwitchValueType>(
                this IQueryBuilder<T> queryBuilder,
                Func<TSwitchValueType> switchValue,
                IDictionary<TSwitchValueType, Expression<Func<T, dynamic>>> orderingPredicateCases,
                IDictionary<TSwitchValueType, SortOrder> sortOrderCases)
                where T : class
        {
            if (orderingPredicateCases == null ||
                orderingPredicateCases.Count.Equals(0))
            {
                return queryBuilder;
            }

            List<SwitchCase> switchCaseList = new List<SwitchCase>();

            foreach (KeyValuePair<TSwitchValueType, Expression<Func<T, dynamic>>> switchCaseTestValuePair in orderingPredicateCases)
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

            Expression<Func<T, dynamic>> defaultValue = (t) => default(TSwitchValueType);

            SwitchExpression switchExpression = Expression.Switch(switchParameterExpression,
                                                                  Expression.Constant(defaultValue),
                                                                  switchCaseList.ToArray());

            Expression<Func<T, dynamic>> queryPredicate =
                 Expression.Lambda<Func<TSwitchValueType, Expression<Func<T, dynamic>>>>
                 (switchExpression, switchParameterExpression).Compile()(switchVariableValue);

            queryBuilder.OrderBy(queryPredicate, sortOrderCases[switchVariableValue]);

            return queryBuilder;
        }

    }
}
