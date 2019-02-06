using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser
{
    public class ValueExpression : Expression
    {
        private int value_;

        public ValueExpression(int value)
        {
            value_ = value;
        }
        public int Evaluate()
        {
            return value_;
        }
    }
}
