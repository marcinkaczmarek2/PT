﻿using CalculationMachine.Data;

namespace CalculationMachine.Logic
{
    public class CalculatorLogic
    {
        public double Add(double a, double b) 
        {
            string calculation = a + " + " + b + " = " + (a+b);
            CalculatorData.AddHistory(calculation);
            return a + b;
        }
        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Division by zero is forbidden");
            }
            string calculation = a + " / " + b + " = " + (a/b);
            CalculatorData.AddHistory(calculation);
            return a / b;
        }
        public double Multiply(double a, double b)
        {
            string calculation = a + " * " + b + " = " + (a*b);
            CalculatorData.AddHistory(calculation);
            return a * b;
        }
        public double Subtruct(double a, double b)
        {
            string calculation = a + " - " + b + " = " +(a-b);
            CalculatorData.AddHistory(calculation);
            return a - b;
        }
    }
}
