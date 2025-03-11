using CalculationMachine;

namespace CalculationMachineTest
{
    [TestClass]
    public sealed class CalculatorTest
    {
        [TestMethod]
        public void TestAdd()
        {
            Calculator calculator = new Calculator();

            double a = 5d;
            double b = 13d;

            double result = calculator.Add(a, b);

            Assert.AreEqual(result, 18);
        }

        [TestMethod]
        public void TestDivide()
        {
            Calculator calculator = new Calculator();

            double a = 26d;
            double b = 13d;

            double result = calculator.Divide(a, b);

            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void TestDivideByZero()
        {
            Calculator calculator = new Calculator();

            double a = 26d;
            double b = 0d;

            Assert.ThrowsException<DivideByZeroException>(() => calculator.Divide(a, b));
        }
        [TestMethod]
        public void TestMultiply()
        {
            Calculator calculator = new Calculator();

            double a = 69d;
            double b = 2d;

            double result = calculator.Multiply(a, b);

            Assert.AreEqual(result, 138);
        }

        [TestMethod]

        public void TestSubtract() 
        {
            Calculator calculator = new Calculator();

            double a = -19d;
            double b = -10d;

            double result = calculator.Subtruct(a, b);

            Assert.AreEqual(result, -9);
        }
    }
}
