namespace DataTest.DataGenerators
{
    using Data.API;

    public class HardCodedDataGenerator : IDataGenerator
    {
        public void GenerateData(IDataRepository repository)
        {
            repository.AddBook(1, "Wiedźmin", "SuperNowa", "Andrzej Sapkowski", 320, "Fantasy");
            repository.AddReader(1, "Jan", "Kowalski", "jan.kowalski@example.com", "123456789", "Student", 0.0m);
            repository.AddState(1, 3, 1);
            repository.AddEvent(1, 1, 1);
        }
    }
}
