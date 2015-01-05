using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.Util;

namespace EApp.Common.Query
{
    public sealed class QueryBuilderHelper
    {
        //public static Expression<Func<T, bool>> ValidatePredicate<T>(Expression<Func<T, bool>> predicate)
        //{
        //    if (Lambda.GetCriteriaCount(predicate) > 1)
        //    {
        //        throw new InvalidOperationException(String.Format("仅允许添加一个条件,条件：{0}", predicate));
        //    }

        //    var value = 1;//predicate.Value();
            
        //    if (value == null)
        //    {
        //        return null;
        //    }

        //    if (string.IsNullOrWhiteSpace(value.ToString()))
        //    {
        //        return null;
        //    }

        //    return predicate;
        //}
    }
}
