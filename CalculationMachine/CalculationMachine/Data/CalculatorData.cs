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
            return new List<string>(calcHistory);
            
        }
        public static void PrintHistory()
        {
            for (int i = 0; i < calcHistory.Count; i++)
            {
                Console.WriteLine(calcHistory[i]);
            }

        }

    }
}