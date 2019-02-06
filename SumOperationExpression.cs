using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser
{
    class SumOperationExpression : OperationExpression
    {
        public SumOperationExpression(Expression leftExpression, Expression rightExpression) : base(leftExpression, rightExpression) { }

        public override int Evaluate()
        {
            return LeftExpression.Evaluate() + RightExpression.Evaluate();
        }
    }
}
