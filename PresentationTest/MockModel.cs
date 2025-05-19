using Presentation.Model.API;
using Services.API;

namespace PresentationTest
{
    public class MockModel : IModel
    {
        private ILibraryService service;

        internal MockModel(ILibraryService? _service = null)
        {
            service = _service ?? ILibraryService.CreateNewService();
        }

        public override List<IBookModelData> GetAllBooks()
        {
            return service.GetAllBooks().ConvertAll(x => (IBookModelData)x);
        }

        public override Task AddBook(int id, string title, string publisher, string author, int numberOfPages, string genre)
        {
            return service.AddBook(id, title, publisher, author, numberOfPages, genre);
        }

        public override Task RemoveBook(int id)
        {
            return service.RemoveBook(id);
        }

        public override List<IReaderModelData> GetAllUsers()
        {
            return service.GetAllReaders().ConvertAll(x => (IReaderModelData)x);
        }

        public override Task AddUser(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt)
        {
            return service.AddReader(id, name, surname, email, phoneNumber, role, debt);
        }

        public override Task RemoveUser(int id)
        {
            return service.RemoveReader(id);
        }

        public override List<IStateModelData> GetAllStates()
        {
            return service.GetAllStates().ConvertAll(x => (IStateModelData)x);
        }

        public override Task AddState(int id, int bookId, int quantity)
        {
            return service.AddState(id, bookId, quantity);
        }

        public override Task RemoveState(int id)
        {
            return service.RemoveState(id);
        }

        public override Task RemoveEvent(int id)
        {
            return service.RemoveEvent(id);
        }

        public override List<IEventModelData> GetAllEvents()
        {
            return service.GetAllEvents().ConvertAll(x => (IEventModelData)x);
        }

        public override Task AddEvent(int eventId, int userId, int bookId)
        {
            return service.AddEvent(eventId, userId, bookId);
        }

        public override Task BorrowBook(int eventId, int userId, int bookId)
        {
            return service.BorrowBook(eventId, userId, bookId);
        }

        public override Task ReturnBook(int eventId, int userId, int bookId)
        {
            return service.ReturnBook(eventId, userId, bookId);
        }
    }
}
