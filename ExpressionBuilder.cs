using System;

namespace ExpressionParser
{
    public static class ExpressionBuilder
    {
        public static Expression BuildBinaryExpression(Expression first, Expression last, Char operation)
        {
            if (operation == '+')
            {
                return new SumOperationExpression(first, last);
            }
            else if (operation == '-')
            {
                return new RestOperationExpression(first, last);
            }
            else if (operation == '*')
            {
                return new MultiplyOperationExpression(first, last);
            }
            else if (operation == '/')
            {
                return new DivideOperationExpression(first, last);
            }

            throw new ArgumentException("Invalid operation");
        }

        public static Expression BuildValueExpression(int value)
        {
            return new ValueExpression(value);
        }
    }
}