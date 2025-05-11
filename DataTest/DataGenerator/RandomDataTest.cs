using Data.API;
using Data.API.Models;
using Data.Catalog;
using Data.Users;
using Data.Events;
using DataTest.TestDataGeneration;

namespace DataTest
{
    [TestClass]
    public class RandomDataTests
    {
        private IData _context;

        [TestInitialize]
        public void Initialize()
        {
            IDataGenerator dataGenerator = new RandomDataGenerator();
            _context = dataGenerator.GetDataContext();
        }

        [TestMethod]
        public void ShouldGenerateBooksAndBoardGames()
        {
            List<IBorrowable> items = _context.GetItems();
            Assert.IsTrue(items.Count > 0, "No items generated.");

            bool hasBook = false;
            bool hasBoardGame = false;

            foreach (Borrowable item in items)
            {
                if (item is Book)
                {
                    hasBook = true;
                }
                if (item is BoardGame)
                {
                    hasBoardGame = true;
                }
            }

            Assert.IsTrue(hasBook, "No books generated.");
            Assert.IsTrue(hasBoardGame, "No board games generated.");
        }

        [TestMethod]
        public void ShouldGenerateReadersAndEmployees()
        {
            List<IUser> users = _context.GetUsers();
            Assert.IsTrue(users.Count > 0, "No users generated.");

            bool hasReader = false;
            bool hasEmployee = false;

            foreach (User user in users)
            {
                if (user is Reader)
                {
                    hasReader = true;
                }
                if (user is Employee)
                {
                    hasEmployee = true;
                }
            }

            Assert.IsTrue(hasReader, "No readers generated.");
            Assert.IsTrue(hasEmployee, "No employees generated.");
        }

        [TestMethod]
        public void ShouldGenerateBorrowAndReturnEvents()
        {
            List<IEvent> events = _context.GetEvents();
            Assert.IsTrue(events.Count > 0, "No events generated.");

            bool hasBorrowEvent = false;
            bool hasReturnEvent = false;

            foreach (EventBase e in events)
            {
                if (e is ItemBorrowedEvent)
                {
                    hasBorrowEvent = true;
                }
                if (e is ItemReturnedEvent)
                {
                    hasReturnEvent = true;
                }
            }

            Assert.IsTrue(hasBorrowEvent, "No borrow events generated.");
            Assert.IsTrue(hasReturnEvent, "No return events generated.");
        }

        [TestMethod]
        public void BorrowedEvents_ShouldHaveValidUsersAndItems()
        {
            List<IEvent> events = _context.GetEvents();
            List<IUser> users = _context.GetUsers();
            List<IBorrowable> items = _context.GetItems();

            foreach (EventBase e in events)
            {
                if (e is ItemBorrowedEvent borrowedEvent)
                {
                    bool userExists = false;
                    bool itemExists = false;

                    foreach (User user in users)
                    {
                        if (user.id == borrowedEvent.userId)
                        {
                            userExists = true;
                            break;
                        }
                    }

                    foreach (Borrowable item in items)
                    {
                        if (item.id == borrowedEvent.itemId)
                        {
                            itemExists = true;
                            break;
                        }
                    }

                    Assert.IsTrue(userExists, "User not found for BorrowedEvent.");
                    Assert.IsTrue(itemExists, "Item not found for BorrowedEvent.");
                }
            }
        }

        [TestMethod]
        public void ReturnedEvents_ShouldHaveValidUsersAndItems()
        {
            List<IEvent> events = _context.GetEvents();
            List<IUser> users = _context.GetUsers();
            List<IBorrowable> items = _context.GetItems();

            foreach (EventBase e in events)
            {
                if (e is ItemReturnedEvent returnedEvent)
                {
                    bool userExists = false;
                    bool itemExists = false;

                    foreach (User user in users)
                    {
                        if (user.id == returnedEvent.userId)
                        {
                            userExists = true;
                            break;
                        }
                    }

                    foreach (Borrowable item in items)
                    {
                        if (item.id == returnedEvent.itemId)
                        {
                            itemExists = true;
                            break;
                        }
                    }

                    Assert.IsTrue(userExists, "User not found for ReturnedEvent.");
                    Assert.IsTrue(itemExists, "Item not found for ReturnedEvent.");
                }
            }
        }
    }
}
