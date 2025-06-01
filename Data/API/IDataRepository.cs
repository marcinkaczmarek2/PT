using Data.Implementations;

namespace Data.API
{
    public abstract class IDataRepository : IDisposable
    {
        // ----------- Book -----------
        public abstract void AddBook(int id, string title, string publisher, string author, int numberOfPages, string genre);
        public abstract void RemoveBook(int id);
        public abstract IBook? GetBook(int id);

        public abstract IEnumerable<IBook> GetAllBooks();
        public abstract IEnumerable<IBook> GetAllBooks(string author);

        // ---------- Reader -----------
        public abstract void AddReader(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt);
        public abstract void RemoveReader(int id);
        public abstract IUser? GetReader(int id);
        public abstract IEnumerable<IUser> GetAllReaders();
        public abstract IEnumerable<IUser> GetAllReaders(string name);

        // ---------- State -----------
        public abstract void AddState(int id, int quantity, int bookId);
        public abstract void RemoveState(int id);
        public abstract IState? GetState(int id);
        public abstract IEnumerable<IState> GetAllStates();
        public abstract IEnumerable<IState> GetAllStates(int bookId);

        // ---------- Event -----------
        public abstract void AddEvent(int id, int userId, int bookId);
        public abstract void RemoveEvent(int id);
        public abstract IEnumerable<IEvent> GetAllEvents();
        public abstract IEnumerable<IEvent> GetAllEvents(int userId);

        //-------------------------------------
        public abstract void ChangeQuantity(int stateId, int change);
        public abstract void ClearAll();

        // Factory
        public static IDataRepository CreateNewRepository()
        {
            return new DataRepository();
        }

        public static IDataRepository CreateNewRepository(string connectionString)
        {
            return new DataRepository(connectionString);
        }

        public abstract void Dispose();
    }
}
