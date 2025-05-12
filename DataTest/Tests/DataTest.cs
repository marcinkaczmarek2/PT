using Data.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayerTest
{
    [TestClass]
    public abstract class DataTest
    {
        protected IData _data;

        [TestInitialize]
        public abstract void Initialize();
    }
}
