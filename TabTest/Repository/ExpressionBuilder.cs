using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnockoutHelpers;
using TabTest.Repository.Filters;

namespace TabTest.Repository
{
    public static class ExpressionBuilder
    {
        public static string GetExpression(string columnName, FilterCondition condition, string value, Type valueType)
        {
            var str = "";

            switch (condition)
            {
                case FilterCondition.ContainsCaseSensitive:
                case FilterCondition.Contains:
                    return string.Format("{0}.Contains(\"{1}\")", columnName, value);
                case FilterCondition.DoesNotContain:
                case FilterCondition.DoesNotContainCaseSensitive:
                    return string.Format("!{0}.Contains(\"{1}\")", columnName, value);
                case FilterCondition.EndsWith:
                case FilterCondition.EndsWithCaseSensitive:
                    return string.Format("{0}.EndsWith(\"{1}\")", columnName, value);
                case FilterCondition.Equal:
                    return string.Format(valueType.IsNumericType() ? "{0} == {1}" : "{0}.Equals(\"{1}\")", columnName, value);
                case FilterCondition.EqualCaseSensitive:
                    return string.Format("{0}.Equals(\"{1}\")", columnName, value);
                case FilterCondition.GreaterThan:
                    return string.Format("{0} > {1}", columnName, value);
                case FilterCondition.GreaterThanOrEqual:
                    return string.Format("{0} >= {1}", columnName, value);
                case FilterCondition.LessThan:
                    return string.Format("{0} < {1}", columnName, value);
                case FilterCondition.LessThanOrEqual:
                    return string.Format("{0} <= {1}", columnName, value);
                case FilterCondition.NotEqual:
                    return string.Format(valueType.IsNumericType() ? "{0} != {1}" : "!{0}.Equals(\"{1}\")", columnName, value);
                case FilterCondition.StartsWith:
                case FilterCondition.StartsWithCaseSensitive:
                    return string.Format("{0}.StartsWith(\"{1}\")", columnName, value);
                case FilterCondition.NotNull:
                    return string.Format("!string.IsNullOrEmpty({0})", columnName);
                case FilterCondition.Null:
                    return string.Format("string.IsNullOrEmpty({0})", columnName);
            }

            return "";
        }

    }
}