using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.Query;

namespace EApp.Common.Lambda
{
    public class LambdaUtil
    {
        #region GetValue(获取值)

        /// <summary>
        /// 获取值,范例：t => t.Name == "A",返回 A
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name == "A"</param>
        public static object GetValue(LambdaExpression expression)
        {
            if (expression == null)
            {
                return null;
            }

            BinaryExpression binaryExpression = GetBinaryExpression(expression);

            if (binaryExpression != null)
            {
                return GetBinaryValue(binaryExpression);
            }

            var methodCallExpression = GetMethodCallExpression(expression);

            if (methodCallExpression != null)
            {
                return GetMethodValue(methodCallExpression);
            }

            return null;
        }

        /// <summary>
        /// 获取二元表达式
        /// </summary>
        public static BinaryExpression GetBinaryExpression(LambdaExpression expression)
        {
            var binaryExpression = expression.Body as BinaryExpression;
            if (binaryExpression != null)
            {
                return binaryExpression;
            }

            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression == null)
            {
                return null;
            }

            return unaryExpression.Operand as BinaryExpression;
        }

        /// <summary>
        /// 获取 MethodCall 表达式 E.g. p=>p.Name.StartWith("ABC");
        /// </summary>
        public static MethodCallExpression GetMethodCallExpression(LambdaExpression expression)
        { 
            var methodCallExpression = expression.Body as MethodCallExpression;

            if (methodCallExpression != null)
            {
                return methodCallExpression;
            }

            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression == null)
            {
                return null;
            }

            return unaryExpression.Operand as MethodCallExpression;
        }

        /// <summary>
        /// 获取 条件 表达式 p => p.AnnualQuotation != null ? p.AnnualQuotation.StartingPrice : p.Id;
        /// </summary>
        public static ConditionalExpression GetConditionalExpression(LambdaExpression expression)
        {
            var conditionalExpression = expression.Body as ConditionalExpression;

            if (conditionalExpression != null)
            {
                return conditionalExpression;
            }

            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression == null)
            {
                return null;
            }

            return unaryExpression.Operand as ConditionalExpression;
        }

        /// <summary>
        /// 获取二元表达式的值
        /// </summary>
        private static object GetBinaryValue(BinaryExpression binaryExpression)
        {
            var unaryExpression = binaryExpression.Right as UnaryExpression;
            if (unaryExpression != null)
            {
                return GetConstantValue(unaryExpression.Operand);
            }

            return GetConstantValue(binaryExpression.Right);
        }

        /// <summary>
        /// 获取常量值
        /// </summary>
        private static object GetConstantValue(Expression expression)
        {
            var constantExpression = expression as ConstantExpression;
            
            if (constantExpression == null)
            {
                return null;
            }

            return constantExpression.Value;
        }

        /// <summary>
        /// 获取方法调用表达式的值
        /// </summary>
        private static object GetMethodValue(MethodCallExpression callExpression)
        {
            var argumentExpression = callExpression.Arguments.FirstOrDefault();

            return GetConstantValue(argumentExpression);
        }

        #endregion

        #region GetInstanceMemberName

        public static string GetMemberName(LambdaExpression expression) 
        {
            if (!(expression.Body is MemberExpression))
            {
                return null;
            }

            MemberExpression memberExpression = expression.Body as MemberExpression;

            string hierarchyPropertyName = memberExpression.Member.Name;

            while (memberExpression.Expression is MemberExpression)
            {
                memberExpression = memberExpression.Expression as MemberExpression;

                hierarchyPropertyName = memberExpression.Member.Name + "." + hierarchyPropertyName;
            }

            return hierarchyPropertyName;
        }

        #endregion

        #region GetCriteriaCount(获取谓词条件的个数)

        /// <summary>
        /// 获取谓词条件的个数
        /// </summary>
        /// <param name="expression">谓词表达式,范例：p => p.Quotation.Info.Number.Equals(100) & p.Quotation.StartingPrice > 100 && (p.AnnualQuotation.StartingPrice > p.AnnualQuotation.StartingPrice && p.AnnualQuotation.StartingPrice < 200)</param>
        public static int GetCriteriaCount(LambdaExpression expression)
        {
            if (expression == null)
                return 0;
            var result = expression.ToString().Replace("AndAlso", "|").Replace("OrElse", "|");
            return result.Split('|').Count();
        }

        #endregion

        #region Parse Expression

        public static Expression<Func<TEntity, bool>> ParsePredicate<TEntity>(string propertyName, object value, Operator @operator) 
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "e");

            MemberExpression memberExpression = Expression.Property(parameterExpression, propertyName);

            ConstantExpression constantExpression = Expression.Constant(value);

            BinaryExpression filterExpression = null;

            if (@operator == Operator.Equal)
            {
                filterExpression = Expression.Equal(memberExpression, constantExpression);
            }
            else if (@operator == Operator.NotEqual)
            {
                filterExpression = Expression.NotEqual(memberExpression, constantExpression);
            }
            else if (@operator == Operator.GreaterThan)
            {
                filterExpression = Expression.GreaterThan(memberExpression, constantExpression);
            }
            else if (@operator == Operator.GreaterThanEqual)
            {
                filterExpression = Expression.GreaterThanOrEqual(memberExpression, constantExpression);
            }
            else if (@operator == Operator.LessThan)
            {
                filterExpression = Expression.LessThan(memberExpression, constantExpression);
            }
            else
            {
                filterExpression = Expression.LessThanOrEqual(memberExpression, constantExpression);
            }

            return Expression.Lambda<Func<TEntity, bool>>(filterExpression, parameterExpression);
        }


        #endregion
    }
}
