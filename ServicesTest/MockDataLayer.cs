using Data.API;

namespace ServicesTest
{
    internal class MockDataLayer : IDataRepository
    {
        public List<IBook> Books = new List<IBook>();
        public List<IUser> Readers = new List<IUser>();
        public List<IState> States = new List<IState>();
        public List<IEvent> Events = new List<IEvent>();

        // ----------- Book -----------

        public override void AddBook(int id, string title, string publisher, string author, int numberOfPages, string genre)
        {
            Books.Add(new MockBook(id, title, publisher, author, numberOfPages, genre));
        }

        public override void RemoveBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.id == id);
            if (book != null) Books.Remove(book);
        }

        public override IBook? GetBook(int id)
        {
            return Books.FirstOrDefault(b => b.id == id);
        }

        public override IEnumerable<IBook> GetAllBooks()
        {
            return Books;
        }

        public override IEnumerable<IBook> GetAllBooks(string author)
        {
            return Books;
        }

        // ----------- Reader -----------

        public override void AddReader(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt)
        {
            Readers.Add(new MockReader(id, name, surname, email, phoneNumber, role));
        }

        public override void RemoveReader(int id)
        {
            var reader = Readers.FirstOrDefault(r => r.id == id);
            if (reader != null) Readers.Remove(reader);
        }

        public override IUser? GetReader(int id)
        {
            return Readers.FirstOrDefault(r => r.id == id);
        }

        public override IEnumerable<IUser> GetAllReaders(string name)
        {
            return Readers;
        }
        public override IEnumerable<IUser> GetAllReaders()
        {
            return Readers;
        }

        // ----------- State -----------

        public override void AddState(int id, int quantity, int bookId)
        {
            States.Add(new MockState(id, bookId, quantity));
        }

        public override void RemoveState(int id)
        {
            var state = States.FirstOrDefault(s => s.stateId == id);
            if (state != null) States.Remove(state);
        }

        public override IState? GetState(int id)
        {
            return States.FirstOrDefault(s => s.stateId == id);
        }

        public override IEnumerable<IState> GetAllStates(int bookId)
        {
            return States;
        }
        public override IEnumerable<IState> GetAllStates()
        {
            return States;
        }

        // ----------- Event -----------

        public override void AddEvent(int id, int userId, int bookId)
        {
            Events.Add(new MockEvent(id, userId, bookId));
        }

        public override void RemoveEvent(int id)
        {
            var ev = Events.FirstOrDefault(e => e.eventId == id);
            if (ev != null) Events.Remove(ev);
        }

        public override IEnumerable<IEvent> GetAllEvents(int userId)
        {
            return Events;
        }
        public override IEnumerable<IEvent> GetAllEvents()
        {
            return Events;
        }

        // ----------- Other -----------

        public override void ChangeQuantity(int stateId, int change)
        {
            var state = States.FirstOrDefault(s => s.stateId == stateId);
            if (state != null)
            {
                state.quantity += change;
            }
        }

        public override void ClearAll()
        {
            Books.Clear();
            Readers.Clear();
            States.Clear();
            Events.Clear();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
