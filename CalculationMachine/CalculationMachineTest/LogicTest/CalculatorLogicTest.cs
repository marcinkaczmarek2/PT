using CalculationMachine.Logic;

namespace CalculationMachineTest.LogicTest
{
    [TestClass]
    public class CalculatorLogicTest
    {
        [TestMethod]
        public void TestAdd()
        {
            CalculatorLogic calculator = new CalculatorLogic();

            double a = 5d;
            double b = 13d;
            
            double result = calculator.Add(a, b);
            Assert.AreEqual(result, 18);
        }

        [TestMethod]
        public void TestDivide()
        {
            CalculatorLogic calculator = new CalculatorLogic();

            double a = 26d;
            double b = 13d;

            double result = calculator.Divide(a, b);

            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void TestDivideByZero()
        {
            CalculatorLogic calculator = new CalculatorLogic();

            double a = 26d;
            double b = 0d;

            Assert.ThrowsException<DivideByZeroException>(() => calculator.Divide(a, b));
        }
        [TestMethod]
        public void TestMultiply()
        {
            CalculatorLogic calculator = new CalculatorLogic();

            double a = 69d;
            double b = 2d;

            double result = calculator.Multiply(a, b);

            Assert.AreEqual(result, 138);
        }

        [TestMethod]

        public void TestSubtract()
        {
            CalculatorLogic calculator = new CalculatorLogic();

            double a = -19d;
            double b = -10d;

            double result = calculator.Subtruct(a, b);

            Assert.AreEqual(result, -9);
        }
    }
}