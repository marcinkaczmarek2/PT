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

            CalculatorData.AddHistory("5 + 13");
            CalculatorData.AddHistory("26 / 13");

            List<string> history = CalculatorData.GetHistory();

            Assert.AreEqual(2, history.Count);
            Assert.AreEqual("5 + 13", history[0]);
            Assert.AreEqual("26 / 13", history[1]);
        }
    }
}
