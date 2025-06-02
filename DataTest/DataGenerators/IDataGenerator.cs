namespace DataTest.DataGenerators
{
    using Data.API;

    public interface IDataGenerator
    {
        void GenerateData(IDataRepository repository);
    }
}
