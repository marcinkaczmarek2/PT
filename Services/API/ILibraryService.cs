using Data.API;

namespace Services.API
{
    public abstract class ILibraryService
    {
        // ------------ Book ------------
        public abstract Task AddBook(int id, string title, string publisher, string author, int numberOfPages, string genre);
        public abstract Task RemoveBook(int id);
        public abstract List<IBookServiceData> GetAllBooks();

        // ----------- Reader ------------
        public abstract Task AddReader(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt);
        public abstract Task RemoveReader(int id);
        public abstract List<IReaderServiceData> GetAllReaders();

        // ---------- State ------------
        public abstract Task AddState(int stateId, int bookId, int quantity);
        public abstract Task RemoveState(int stateId);
        public abstract List<IStateServiceData> GetAllStates();

        // ---------- Event ------------
        public abstract Task AddEvent(int eventId, int userId, int bookId);
        public abstract Task RemoveEvent(int eventId);
        public abstract List<IEventServiceData> GetAllEvents();

        // ----------------------------------------
        // ------------ Business Logic ------------

        public abstract Task BorrowBook(int eventId, int userId, int bookId);
        public abstract Task ReturnBook(int eventId, int userId, int bookId);

        // Factory methods
        public static ILibraryService CreateNewService(IDataRepository data)
        {
            return new Service.Implementation.LibraryService(data);
        }

        public static ILibraryService CreateNewService()
        {
            return new Service.Implementation.LibraryService();
        }
    }
}
