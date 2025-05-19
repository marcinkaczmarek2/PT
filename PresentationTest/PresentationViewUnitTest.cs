using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.ViewModel;
using PresentationTest;

namespace Presentation.Test
{
    [TestClass]
    public sealed class PresentationViewUnitTest
    {
        private readonly MockModel model = new MockModel();

        [TestMethod]
        public void BookPresentation()
        {
            VMBookList list = new VMBookList(model);
            VMBook book = new VMBook(1, "Test Book", "Test Publisher", "Test Author", 123, "Fiction");

            model.AddBook(2, "Temp", "TempPub", "TempAuth", 1, "Genre");
            list.SelectedVM.Add(book);

            Assert.IsTrue(list.SelectedVM.Count == 1);
            Assert.AreEqual(1, list.SelectedVM[0].Id);
            Assert.AreEqual("Test Book", list.SelectedVM[0].Title);
            Assert.AreEqual("Test Publisher", list.SelectedVM[0].Publisher);
            Assert.AreEqual("Test Author", list.SelectedVM[0].Author);
            Assert.AreEqual(123, list.SelectedVM[0].NumberOfPages);
            Assert.AreEqual("Fiction", list.SelectedVM[0].Genre);

            model.RemoveBook(2);
            list.SelectedVM.Remove(book);
            Assert.IsTrue(list.SelectedVM.Count == 0);
        }

        [TestMethod]
        public void ReaderPresentation()
        {
            VMReaderList list = new VMReaderList(model);
            VMReader reader = new VMReader(1, "Test", "Test", "test@example.com", "123456789", "Reader", 0m);

            model.AddUser(2, "Temp", "Temp", "temp@example.com", "000000000", "Reader", 0m);
            list.ReaderView.Add(reader);

            Assert.AreEqual(1, list.ReaderView.Count);
            Assert.AreEqual(1, list.ReaderView[0].Id);
            Assert.AreEqual("Test", list.ReaderView[0].Name);
            Assert.AreEqual("Test", list.ReaderView[0].Surname);
            Assert.AreEqual("test@example.com", list.ReaderView[0].Email);
            Assert.AreEqual("123456789", list.ReaderView[0].PhoneNumber);
            Assert.AreEqual("Reader", list.ReaderView[0].Role);
            Assert.AreEqual(0m, list.ReaderView[0].Debt);

            model.RemoveUser(2);
            list.ReaderView.Remove(reader);
            Assert.AreEqual(0, list.ReaderView.Count);
        }

        [TestMethod]
        public void StatePresentation()
        {
            VMStateList list = new VMStateList(model);
            VMState state = new VMState(1, 10, 1);

            model.AddState(2, 5, 2);
            list.StateView.Add(state);

            Assert.AreEqual(1, list.StateView.Count);
            Assert.AreEqual(1, list.StateView[0].StateId);
            Assert.AreEqual(10, list.StateView[0].Quantity);
            Assert.AreEqual(1, list.StateView[0].BookId);

            model.RemoveState(2);
            list.StateView.Remove(state);
            Assert.AreEqual(0, list.StateView.Count);
        }

        [TestMethod]
        public void EventPresentation()
        {
            VMEventList list = new VMEventList(model);
            VMEvent ev = new VMEvent(1, 1, 1);

            model.AddEvent(2, 1, 1);
            list.SelectedVM.Add(ev);

            Assert.AreEqual(1, list.SelectedVM.Count);
            Assert.AreEqual(1, list.SelectedVM[0].Id);
            Assert.AreEqual(1, list.SelectedVM[0].UserId);
            Assert.AreEqual(1, list.SelectedVM[0].BookId);

            model.RemoveEvent(2);
            list.SelectedVM.Remove(ev);
            Assert.AreEqual(0, list.SelectedVM.Count);
        }
    }
}
