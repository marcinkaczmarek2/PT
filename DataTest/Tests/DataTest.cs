using Data.API;

namespace DataTest
{
    [TestClass]
    public abstract class DataTest
    {
        protected IData _data;

        [TestInitialize]
        public abstract void Initialize();
    }
}
