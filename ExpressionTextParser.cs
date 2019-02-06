using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionParser
{
    public class ExpressionTextParser
    {
        public static Expression Parse(string expressionString)
        {
            string rest;

            Expression expression = ParseExpressionUntilClosure(expressionString, ' ', out rest);
            //this is imposible to happen because the expressionString is completely consumed 
            //but if is happening something is wrong
            if (rest != "")
            {
                throw new ApplicationException("The expression string was not consumed completely");
            }
            return expression;
        }

        static Expression ParseExpressionUntilClosure(String expressionString, Char closure, out String rest)
        {

            rest = expressionString;
            List<Expression> listOfExpressions = new List<Expression>();
            List<char> listOfOperations = new List<char>();

            while (rest.Any())
            {
                Char c = rest[0];

                if (isOpositeClosure(closure, c))
                {
                    rest = rest.Remove(0, 1);
                    break;
                }
                else if (isOperation(c))
                {
                    listOfOperations.Add(c);
                    rest = rest.Remove(0, 1);
                }
                else if (Char.IsNumber(c))
                {
                    int firstOperationCharIndex = FindNextSimbolIndex(rest);
                    //reads and removes the number untill the next character or completely if no character found
                    if (firstOperationCharIndex > 0)
                    {
                        string number = rest.Substring(0, firstOperationCharIndex);
                        listOfExpressions.Add(ExpressionBuilder.BuildValueExpression(int.Parse(number)));
                        rest = rest.Remove(0, firstOperationCharIndex);
                        c = rest[0];
                        listOfOperations.Add(c);
                        if (!isClosure(c))
                        {
                            rest = rest.Remove(0, 1);
                        }

                    }
                    else
                    {
                        string number = rest;
                        listOfExpressions.Add(ExpressionBuilder.BuildValueExpression(int.Parse(number)));
                        rest = "";
                    }
                }
                else if (c == '(' || c == '[' || c == '{')
                {
                    rest = rest.Remove(0, 1);
                    listOfExpressions.Add(ParseExpressionUntilClosure(rest, c, out rest));
                }

                //if no character is left for consume and the string is not correctly closed throws syntax error
                if (!rest.Any() && closure != ' ')
                {
                    throw new ArgumentException("Syntax Error");
                }
            }

            Expression expression = ConvertToExpression(listOfExpressions, listOfOperations);

            return expression;
        }


        static private bool isOpositeClosure(char closure, char c)
        {
            if (closure == '(') { return c == ')'; }
            if (closure == '[') { return c == ']'; }
            if (closure == '{') { return c == '}'; }
            return false;
        }
        
        private static bool isOperation(char c)
        {
            if (c == '+' || c == '-' || c == '/' || c == '*')
            {
                return true;
            }
            return false;
        }

        private static bool isClosure(char c)
        {
            if (c == ')' || c == ']' || c == '}')
            {
                return true;
            }
            return false;
        }

        //both lists represents the expressions and operation at the current level
        static private Expression ConvertToExpression(List<Expression> listOfExpressions, List<char> listOfOperations)
        {
            //if the expression is unary or binary, returns a new expression
            if (listOfExpressions.Count == 1)
            {
                return listOfExpressions.First();
            }
            if (listOfExpressions.Count == 2)
            {
                return ExpressionBuilder.BuildBinaryExpression(listOfExpressions.First(), listOfExpressions.Last(), listOfOperations.First());
            }

            int prioritaryIndex = FindPrioritaryIndex(listOfOperations);

            //creates a new expression at the index of the first prioritary operation and replace 
            //the participants in the expression list with just a expression, and removes the operation
            Expression expression = ExpressionBuilder.BuildBinaryExpression(listOfExpressions[prioritaryIndex], listOfExpressions[prioritaryIndex + 1], listOfOperations[prioritaryIndex]);

            listOfExpressions.RemoveRange(prioritaryIndex, 2);
            listOfExpressions.Insert(prioritaryIndex, expression);
            listOfOperations.RemoveAt(prioritaryIndex);

            return ConvertToExpression(listOfExpressions, listOfOperations);
        }


        static private int FindPrioritaryIndex(List<char> listOfOperations)
        {
            //first multiplications and divisions, then addition and substraction
            int firstIndexM = listOfOperations.IndexOf('*');
            int firstIndexD = listOfOperations.IndexOf('/');
            if (firstIndexM != -1 && firstIndexD != -1) { return Math.Min(firstIndexM, firstIndexD); }
            else if (firstIndexM != -1) { return firstIndexM; }
            else if (firstIndexD != -1) { return firstIndexD; }

            int firstIndexP = listOfOperations.IndexOf('+');
            int firstIndexR = listOfOperations.IndexOf('-');
            if (firstIndexP != -1 && firstIndexR != -1) { return Math.Min(firstIndexP, firstIndexR); }
            else if (firstIndexP != -1) { return firstIndexP; }
            else if (firstIndexR != -1) { return firstIndexR; }

            throw new ArgumentException("No valid operations found");
        }



        //to find the whole number if a valid simbol is next
        static private int FindNextSimbolIndex(string expressionString)
        {
            int index = 0;
            foreach (var c in expressionString)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/' || c == ')' || c == ']' || c == '}')
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }


}
