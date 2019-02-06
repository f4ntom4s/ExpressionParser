using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser
{
    public abstract class OperationExpression : Expression
    {
        Expression leftExpression_;
        Expression rightExpression_;

        protected OperationExpression(Expression leftExpression, Expression rightExpression)
        {
            leftExpression_ = leftExpression;
            rightExpression_ = rightExpression;
        }

        public Expression LeftExpression
        {
            get
            {
                return leftExpression_;
            }
        }

        public Expression RightExpression
        {
            get
            {
                return rightExpression_;
            }
        }

        public abstract int Evaluate();
    }
}
