using CalculationMachine.Data;

namespace CalculationMachineTest.DataTest
{
    [TestClass]
    public class CalculatorDataTest
    {
        [TestMethod]
        public void GetHistoryTest()
        {
            CalculatorData.calcHistory.Clear();

            CalculatorData.AddHistory("5 + 13 = 18");
            CalculatorData.AddHistory("5 - 13 = -8");
            CalculatorData.AddHistory("26 / 13 = 2");
            CalculatorData.AddHistory("26 * 13 = 338");

            List<string> history = CalculatorData.GetHistory();

            Assert.AreEqual(4, history.Count);
            Assert.AreEqual("5 + 13 = 18", history[0]);
            Assert.AreEqual("5 - 13 = -8", history[1]);
            Assert.AreEqual("26 / 13 = 2", history[2]);
            Assert.AreEqual("26 * 13 = 338", history[3]);
        }
    }
}
