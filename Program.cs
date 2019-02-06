﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() > 0)
            {
                try
                {
                    string path = args[0];
                    using (var fileStream = new FileStream(path, FileMode.Open))
                    {
                        using (var stream = new StreamReader(fileStream))
                        {
                            while (stream.Peek() >= 0)
                            {
                                try
                                {
                                    string expression = stream.ReadLine();
                                    Console.Out.Write(expression + " = ");
                                    Console.Out.WriteLine(Calculator.Evaluate(expression));
                                }
                                catch(Exception e)
                                {
                                    Console.Out.WriteLine(e.Message.ToString());
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}