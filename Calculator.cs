using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser
{
    public class Calculator
    {
        public static int Evaluate(String expressionString)
        {
            Expression expression;

            expression = ExpressionTextParser.Parse(expressionString);

            return expression.Evaluate();
            
        }
        public static int Evaluate(Expression expression){
            return expression.Evaluate();
        }
    }
}
