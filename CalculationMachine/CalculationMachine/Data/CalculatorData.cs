namespace CalculationMachine.Data
{
    public class CalculatorData
    {
        public static List<string> calcHistory = new List<string>();
        public static void AddHistory(string calculation)
        {
            calcHistory.Add(calculation);
        }

        public static List<string> GetHistory()
        {
            return calcHistory;

        }
    }
}