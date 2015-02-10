using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using EApp.Core;
using EApp.Data.Mapping;

namespace EApp.Data.Queries
{
    /// <summary>
    /// Represents the base class of all the where clause builders.
    /// </summary>
    /// <typeparam name="TDataObject">The type of the data object which would be mapped to
    /// a certain table in the relational database.</typeparam>
    public abstract class WhereClauseBuilder<TDataObject> : ExpressionVisitor, IWhereClauseBuilder<TDataObject>, IOrderByClauseBuilder<TDataObject>
        where TDataObject : class, new()
    {
        #region Private Fields
        private readonly StringBuilder whereClauseBuilder = new StringBuilder();
        private readonly StringBuilder orderByClauseBuilder = new StringBuilder();

        private readonly Dictionary<string, object> parameterValues = new Dictionary<string, object>();
        private readonly IObjectMappingResolver mappingResolver = null;
        private bool startsWith = false;
        private bool endsWith = false;
        private bool contains = false;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>WhereClauseBuilderBase&lt;T&gt;</c> class.
        /// </summary>
        /// <param name="mappingResolver">The <c>Apworks.Storage.IStorageMappingResolver</c>
        /// instance which will be used for generating the mapped field names.</param>
        public WhereClauseBuilder(IObjectMappingResolver mappingResolver)
        {
            this.mappingResolver = mappingResolver;
        }

        public WhereClauseBuilder() { }

        #endregion

        #region Private Methods
        private void Out(string s)
        {
            whereClauseBuilder.Append(s);
        }

        private void OutMember(Expression instance, MemberInfo member)
        {
            string mappedFieldName = mappingResolver.ResolveFieldName<TDataObject>(member.Name);
            Out(mappedFieldName);
        }

        private string ResolveFieldName(Type entityType, MemberInfo member) 
        {
            if (this.mappingResolver == null)
            {
                return MetaDataManager.Instance.ResolveFieldName(entityType.Name, member.Name);
            }
            else
            {
                return this.mappingResolver.ResolveFieldName(entityType.Name, member.Name);
            }
        }

        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the AND operation in the WHERE clause.
        /// </summary>
        protected virtual string And
        {
            get { return "AND"; }
        }
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the OR operation in the WHERE clause.
        /// </summary>
        protected virtual string Or
        {
            get { return "OR"; }
        }
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the EQUAL operation in the WHERE clause.
        /// </summary>
        protected virtual string Equal
        {
            get { return "="; }
        }
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the NOT operation in the WHERE clause.
        /// </summary>
        protected virtual string Not
        {
            get { return "NOT"; }
        }
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the NOT EQUAL operation in the WHERE clause.
        /// </summary>
        protected virtual string NotEqual
        {
            get { return "<>"; }
        }
        /// <summary>
        /// Gets a <c>System.String</c> value which represents the LIKE operation in the WHERE clause.
        /// </summary>
        protected virtual string Like
        {
            get { return "LIKE"; }
        }
        /// <summary>
        /// Gets a <c>System.Char</c> value which represents the place-holder for the wildcard in the LIKE operation.
        /// </summary>
        protected virtual char LikeSymbol
        {
            get { return '%'; }
        }
        /// <summary>
        /// Gets a <c>System.Char</c> value which represents the leading character to be used by the
        /// database parameter.
        /// </summary>
        protected internal abstract char ParameterChar { get; }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.BinaryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            string str;
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    str = "+";
                    break;
                case ExpressionType.AddChecked:
                    str = "+";
                    break;
                case ExpressionType.AndAlso:
                    str = this.And;
                    break;
                case ExpressionType.Divide:
                    str = "/";
                    break;
                case ExpressionType.Equal:
                    str = this.Equal;
                    break;
                case ExpressionType.GreaterThan:
                    str = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    str = ">=";
                    break;
                case ExpressionType.LessThan:
                    str = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    str = "<=";
                    break;
                case ExpressionType.Modulo:
                    str = "%";
                    break;
                case ExpressionType.Multiply:
                    str = "*";
                    break;
                case ExpressionType.MultiplyChecked:
                    str = "*";
                    break;
                case ExpressionType.Not:
                    str = this.Not;
                    break;
                case ExpressionType.NotEqual:
                    str = this.NotEqual;
                    break;
                case ExpressionType.OrElse:
                    str = this.Or;
                    break;
                case ExpressionType.Subtract:
                    str = "-";
                    break;
                case ExpressionType.SubtractChecked:
                    str = "-";
                    break;
                default:
                    throw new NotSupportedException(string.Format(Resources.EX_EXPRESSION_NODE_TYPE_NOT_SUPPORT, node.NodeType.ToString()));
            }

            Out("(");
            Visit(node.Left);
            Out(" ");
            Out(str);
            Out(" ");
            Visit(node.Right);
            Out(")");
            return node;
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            if ((node.Member.DeclaringType == typeof(TDataObject) ||
                 typeof(TDataObject).IsSubclassOf(node.Member.DeclaringType)) &&
                 node.Expression != null &&
                 node.Expression is ParameterExpression)
            {
                string mappedFieldName = this.ResolveFieldName(node.Expression.Type, node.Member);

                Out(mappedFieldName);
            }
            //else if (node.Expression != null &&
            //         node.Expression is MemberExpression &&
            //        (node.Member.DeclaringType.IsAssignableFrom(node.Expression.Type) ||
            //         node.Expression.Type.IsSubclassOf(node.Member.DeclaringType)))
            //{
            //    string mappedFieldName = this.ResolveFieldName(node.Expression.Type, node.Member);

            //    Out(mappedFieldName);
            //}
            else
            {
                object memberValue = null;

                if (node.Member is FieldInfo)
                {
                    ConstantExpression ce = node.Expression as ConstantExpression;
                    FieldInfo fi = node.Member as FieldInfo;
                    memberValue = fi.GetValue(ce.Value);
                }
                else if (node.Member is PropertyInfo)
                {
                    PropertyInfo pi = node.Member as PropertyInfo;

                    if (pi.GetGetMethod().IsStatic)
                    {
                        memberValue = pi.GetValue(null, null);
                    }
                    else
                    {
                        List<PropertyInfo> piList = new List<PropertyInfo>(new PropertyInfo[] { pi });

                        Expression nodeExpression = node.Expression;

                        while (nodeExpression is MemberExpression)
                        {
                            MemberExpression memberExpression = (MemberExpression)nodeExpression;

                            if (memberExpression.Expression is ConstantExpression)
                            {
                                break;
                            }
                            else
                            {
                                if (memberExpression.Member is PropertyInfo)
                                {
                                    PropertyInfo subPi = memberExpression.Member as PropertyInfo;
                                    piList.Add(subPi);
                                }
                            }

                            nodeExpression = memberExpression.Expression;
                        }
                        
                        MemberExpression lastMemberExpression = (MemberExpression)nodeExpression;

                        ConstantExpression constantExpression = lastMemberExpression.Expression as ConstantExpression;

                        FieldInfo fi = lastMemberExpression.Member as FieldInfo;

                        memberValue = fi.GetValue(constantExpression.Value);

                        piList.Reverse();

                        for (int piIndex = 0; piIndex < piList.Count; piIndex++)
                        {
                            memberValue = piList[piIndex].GetValue(memberValue, null);
                        }

                        //throw new NotSupportedException(string.Format(Resources.EX_MEMBER_TYPE_NOT_SUPPORT, node.Member.GetType().FullName));
                    }
                }

                Expression constantExpr = Expression.Constant(memberValue);
                Visit(constantExpr);
            }
            return node;
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ConstantExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            string paramName = string.Format("{0}{1}", ParameterChar, Utils.GetUniqueIdentifier(5));
            Out(paramName);
            if (!parameterValues.ContainsKey(paramName))
            {
                object v = null;
                if (startsWith && node.Value is string)
                {
                    startsWith = false;
                    v = node.Value.ToString() + LikeSymbol;
                }
                else if (endsWith && node.Value is string)
                {
                    endsWith = false;
                    v = LikeSymbol + node.Value.ToString();
                }
                else if (contains && node.Value is string)
                {
                    contains = false;
                    v = LikeSymbol + node.Value.ToString() + LikeSymbol;
                }
                else
                {
                    v = node.Value;
                }
                parameterValues.Add(paramName, v);
            }
            return node;
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MethodCallExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Out("(");
            Visit(node.Object);
            if (node.Arguments == null || node.Arguments.Count != 1)
                throw new NotSupportedException(Resources.EX_INVALID_METHOD_CALL_ARGUMENT_NUMBER);
            Expression expr = node.Arguments[0];
            switch (node.Method.Name)
            {
                case "StartsWith":
                    startsWith = true;
                    Out(" ");
                    Out(Like);
                    Out(" ");
                    break;
                case "EndsWith":
                    endsWith = true;
                    Out(" ");
                    Out(Like);
                    Out(" ");
                    break;
                case "Equals":
                    Out(" ");
                    Out(Equal);
                    Out(" ");
                    break;
                case "Contains":
                    contains = true;
                    Out(" ");
                    Out(Like);
                    Out(" ");
                    break;
                default:
                    throw new NotSupportedException(string.Format(Resources.EX_METHOD_NOT_SUPPORT, node.Method.Name));
            }
            if (expr is ConstantExpression || expr is MemberExpression)
                Visit(expr);
            else
                throw new NotSupportedException(string.Format(Resources.EX_METHOD_CALL_ARGUMENT_TYPE_NOT_SUPPORT, expr.GetType().ToString()));
            Out(")");
            return node;
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.BlockExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitBlock(BlockExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.CatchBlock"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ConditionalExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.DebugInfoExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.DefaultExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitDefault(DefaultExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.DynamicExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitDynamic(System.Linq.Expressions.DynamicExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ElementInit"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.GotoExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitGoto(GotoExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.Expression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitExtension(Expression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.IndexExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitIndex(IndexExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.InvocationExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.LabelExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitLabel(LabelExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.LabelTarget"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.Expression&lt;T&gt;"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ListInitExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitListInit(ListInitExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.LoopExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitLoop(LoopExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberAssignment"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberBinding"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberInitExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberListBinding"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberMemberBinding"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.NewExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitNew(NewExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.NewArrayExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ParameterExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.RuntimeVariablesExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.SwitchExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.SwitchCase"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.TryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitTry(TryExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.TypeBinaryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }
        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.UnaryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.Operand != null)
            {
                Expression operandExpression = node.Operand;

                if (operandExpression is MemberExpression)
                {
                    MemberExpression memberExpression = (MemberExpression)operandExpression;

                    if (memberExpression.Expression != null &&
                        memberExpression.Expression.Type != null &&
                       (memberExpression.Member.DeclaringType == typeof(TDataObject) ||
                        memberExpression.Member.DeclaringType.IsAssignableFrom(memberExpression.Expression.Type) ||
                        memberExpression.Expression.Type.IsSubclassOf(memberExpression.Member.DeclaringType)))
                    {
                        string fieldName = this.ResolveFieldName(memberExpression.Expression.Type, memberExpression.Member);

                        this.orderByClauseBuilder.Append(fieldName);
                    }
                }
            }

            return node;
        }
        #endregion

        #region IWhereClauseBuilder<T> Members
        /// <summary>
        /// Builds the WHERE clause from the given expression object.
        /// </summary>
        /// <param name="expression">The expression object.</param>
        /// <returns>The <c>Apworks.Storage.Builders.WhereClauseBuildResult</c> instance
        /// which contains the build result.</returns>
        public WhereClauseBuildResult BuildWhereClause(Expression<Func<TDataObject, bool>> expression)
        {
            this.whereClauseBuilder.Clear();
            this.parameterValues.Clear();
            this.Visit(expression.Body);
            WhereClauseBuildResult result = new WhereClauseBuildResult
            {
                ParameterValues = parameterValues,
                WhereClause = whereClauseBuilder.ToString()
            };
            return result;
        }
        #endregion


        #region IWhereClauseBuilder<T> Members

        public string BuildOrderByClause(Expression<Func<TDataObject, dynamic>> expression)
        {
            this.orderByClauseBuilder.Clear();
            this.VisitUnary((UnaryExpression)expression.Body);

            return this.orderByClauseBuilder.ToString();
        }

        #endregion
    }
}
