namespace CalculationMachine
{
    public class Calculator
    {
        public double Add(double a, double b) 
        {
            return a + b;
        }
        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Division by zero is forbidden");
            }
            return a / b;
        }
    }
}
