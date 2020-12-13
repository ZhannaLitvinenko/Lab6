using MathNet.Symbolics;
using System;
using System.Collections.Generic;

namespace Lab6
{
    public static class MathHelper
    {
        public static double EvaluateFunction(double t, double y, string function)
        {
            SymbolicExpression expression = Infix.ParseOrThrow(function);
            Dictionary<string, FloatingPoint> variables = new Dictionary<string, FloatingPoint>
            {
                { "t", t },
                { "y", y}
            };

            double result;
            try
            {
                result = expression.Evaluate(variables).RealValue;
            }
            catch (Exception)
            {
                result = 0;
            }

            return result;
        }
    }
}
