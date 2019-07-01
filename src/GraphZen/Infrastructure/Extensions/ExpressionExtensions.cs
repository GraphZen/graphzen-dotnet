// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Linq.Expressions;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure.Extensions
{
    internal static class ExpressionExtensions
    {
        [NotNull]
        public static Func<T, TResult> GetFuncFromExpression<T, TResult>(
            [NotNull] this Expression<Func<T, TResult>> propertySelector) => propertySelector.Compile();

        [NotNull]
        public static PropertyInfo GetPropertyInfoFromExpression<T, TResult>(
            [NotNull] this Expression<Func<T, TResult>> propertySelector)
        {
            MemberExpression exp;

            //this line is necessary, because sometimes the expression comes in as Convert(originalexpression)
            if (propertySelector.Body is UnaryExpression unExp)
            {
                if (unExp.Operand is MemberExpression expression)
                {
                    exp = expression;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else if (propertySelector.Body is MemberExpression expression)
            {
                exp = expression;
            }
            else
            {
                throw new ArgumentException();
            }

            return (PropertyInfo) exp.Member;
        }
    }
}