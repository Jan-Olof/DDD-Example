using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Utilities.Expressions
{
    /// <summary>
    /// Convert from Expression {Func{T1,bool}} to Expression{Func{T2,bool}}.
    /// http://stackoverflow.com/questions/15212779/convert-expressionfunct1-bool-to-expressionfunct2-bool-dynamically
    /// </summary>
    public static class ChangeType
    {
        /// <summary>
        /// Convert from Expression {Func{T1,bool}} to Expression{Func{T2,bool}}.
        /// Lets you pass in the new input type as an argument.
        /// </summary>
        public static LambdaExpression ChangeInputType<T, TResult>(Expression<Func<T, TResult>> expression, Type newInputType)
        {
            if (!typeof(T).IsAssignableFrom(newInputType))
            {
                throw new Exception(string.Format("{0} is not assignable from {1}.", typeof(T), newInputType));
            }

            var beforeParameter = expression.Parameters.Single();
            var afterParameter = Expression.Parameter(newInputType, beforeParameter.Name);
            var visitor = new SubstitutionExpressionVisitor(beforeParameter, afterParameter);
            return Expression.Lambda(visitor.Visit(expression.Body), afterParameter);
        }

        /// <summary>
        /// Convert from Expression {Func{T1,bool}} to Expression{Func{T2,bool}}.
        /// Lets you pass in the input type as a generic parameter and get a strongly typed LambdaExpression.
        /// </summary>
        public static Expression<Func<T2, TResult>> ChangeInputType<T1, T2, TResult>(Expression<Func<T1, TResult>> expression)
        {
            if (!typeof(T1).IsAssignableFrom(typeof(T2)))
            {
                throw new Exception(string.Format("{0} is not assignable from {1}.", typeof(T1), typeof(T2)));
            }

            var beforeParameter = expression.Parameters.Single();
            var afterParameter = Expression.Parameter(typeof(T2), beforeParameter.Name);
            var visitor = new SubstitutionExpressionVisitor(beforeParameter, afterParameter);
            return Expression.Lambda<Func<T2, TResult>>(visitor.Visit(expression.Body), afterParameter);
        }
    }
}