using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.Model.API;

namespace Presentation.Test
{
    [TestClass]
    public sealed class PresentationUnitTest
    {
        private readonly IModel model = IModel.CreateNewModel();

        [TestMethod]
        public async Task UserModelTest()
        {
            await model.AddUser(10, "Test", "Test", "test@example.com", "123456789", "TestRole", 0m);
            var users = model.GetAllUsers();
            Assert.IsTrue(users.Exists(u => u.id == 10));
            await model.RemoveUser(10);
        }

        [TestMethod]
        public async Task BookModelTest()
        {
            await model.AddBook(10, "TestBook", "TestPub", "TestAuthor", 123, "Fiction");
            var books = model.GetAllBooks();
            Assert.IsTrue(books.Exists(b => b.id == 10));
            await model.RemoveBook(10);
        }

        [TestMethod]
        public async Task StateModelTest()
        {
            await model.AddState(10, 10, 5);
            var states = model.GetAllStates();
            Assert.IsTrue(states.Exists(s => s.stateId == 10));
            await model.RemoveState(10);
        }

        [TestMethod]
        public async Task EventModelTest()
        {
            await model.AddEvent(10, 1, 1);
            var events = model.GetAllEvents();
            Assert.IsTrue(events.Exists(e => e.eventId == 10));
            await model.RemoveEvent(10);
        }
    }
}
