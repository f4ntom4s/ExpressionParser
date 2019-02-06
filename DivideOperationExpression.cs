using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser
{
    class DivideOperationExpression : OperationExpression
    {
        public DivideOperationExpression(Expression leftExpression, Expression rightExpression) : base(leftExpression, rightExpression)
        {
        }

        public override int Evaluate()
        {
            if(RightExpression.Evaluate() == 0)
            {
                throw new DivideByZeroException("divide by zero exception");
            }
            return LeftExpression.Evaluate() / RightExpression.Evaluate();
        }
    }
}
