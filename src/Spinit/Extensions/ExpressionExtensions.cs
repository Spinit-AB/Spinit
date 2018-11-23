using System;
using System.Linq.Expressions;

namespace Spinit.Extensions
{
    public static class ExpressionExtensions
    {
        public static string GetName<TModel, TType>(this Expression<Func<TModel, TType>> expression)
            where TModel : class
        {
            return new ExpressionNameVisitor().Visit(expression.Body);
        }

        public static string GetName<TModel>(this Expression<Func<TModel, object>> expression) 
            where TModel : class
        {
            return new ExpressionNameVisitor().Visit(expression.Body);
        }

        private class ExpressionNameVisitor
        {
            public string Visit(Expression expression)
            {
                if (expression is UnaryExpression)
                {
                    expression = ((UnaryExpression)expression).Operand;
                }

                if (expression is MethodCallExpression)
                {
                    return Visit((MethodCallExpression)expression);
                }

                if (expression is MemberExpression)
                {
                    return Visit((MemberExpression)expression);
                }

                if (expression is BinaryExpression && expression.NodeType == ExpressionType.ArrayIndex)
                {
                    return Visit((BinaryExpression)expression);
                }

                return null;
            }

            private string Visit(BinaryExpression expression)
            {
                string result = null;
                if (expression.Left is MemberExpression)
                {
                    result = Visit((MemberExpression)expression.Left);
                }

                var index = Expression.Lambda(expression.Right)
                                      .Compile()
                                      .DynamicInvoke();
                return result + $"[{index}]";
            }

            private string Visit(MemberExpression expression)
            {
                var name = expression.Member.Name;
                var ancestorName = Visit(expression.Expression);
                if (ancestorName != null)
                {
                    name = ancestorName + "." + name;
                }

                return name;
            }

            private string Visit(MethodCallExpression expression)
            {
                string name = null;
                if (expression.Object is MemberExpression)
                {
                    name = Visit((MemberExpression)expression.Object);
                }

                if (expression.Method.Name == "get_Item" && expression.Arguments.Count == 1)
                {
                    var index = Expression.Lambda(expression.Arguments[0])
                                          .Compile()
                                          .DynamicInvoke();
                    name += $"[{index}]";
                }

                return name;
            }
        }
    }
}