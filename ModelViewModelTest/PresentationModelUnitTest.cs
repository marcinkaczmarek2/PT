﻿
using ModelViewModel.Model.API;

namespace ModelViewModel.Model.Test
{
    [TestClass]
    public sealed class PresentationModelUnitTest
    {
        private readonly IModel model = IModel.CreateNewModel();

        [TestMethod]
        public void UserModelTest()
        {
            model.AddUser(10, "Test", "Test", "test@example.com", "123456789", "TestRole", 0m);
            Assert.IsNotNull(model);
            model.RemoveUser(10);
        }

        [TestMethod]
        public void BookModelTest()
        {
            model.AddBook(10, "TestBook", "TestPub", "TestAuthor", 123, "Fiction");
            Assert.IsNotNull(model);
            model.RemoveBook(10);
        }

        [TestMethod]
        public void StateModelTest()
        {
            model.AddState(10, 10, 5);
            Assert.IsNotNull(model);
            model.RemoveState(10);
        }

        [TestMethod]
        public void EventModelTest()
        {
            model.AddEvent(10, 1, 1);
            Assert.IsNotNull(model);
            model.RemoveEvent(10);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (model is IDisposable disposable)
            {
                Console.WriteLine("[DEBUG] Disposing DataRepository and closing DB connection.");
                disposable.Dispose();
            }
        }

    }
}
