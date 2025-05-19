using Data.API;
using Data.Database;

namespace Data.Implementations
{
    internal class DataRepository : IDataRepository
    {
        private LibraryLinqDataContext _context;

        public DataRepository(string? customConnectionString = null)
        {
            if (customConnectionString != null)
            {
                _context = new LibraryLinqDataContext(customConnectionString);
            }
            else
            {
                _context = new LibraryLinqDataContext();
            }
        }

        // ------------------ Book ------------------

        private IBook? EntryToObj(BookDB bookDB)
        {
            if (bookDB != null)
            {
                return new Book(bookDB.id, bookDB.title, bookDB.publisher, bookDB.author, bookDB.numberOfPages ?? 0, bookDB.genre);
            }
            return null;
        }

        public override void AddBook(int id, string title, string publisher, string author, int numberOfPages, string genre)
        {
            BookDB bookDB = new BookDB
            {
                id = id,
                title = title,
                publisher = publisher,
                author = author,
                numberOfPages = numberOfPages,
                genre = genre
            };
            _context.BookDB.InsertOnSubmit(bookDB);
            _context.SubmitChanges();
        }

        public override void RemoveBook(int id)
        {
            BookDB bookDB = _context.BookDB.Single(b => b.id == id);
            _context.BookDB.DeleteOnSubmit(bookDB);
            _context.SubmitChanges();
        }

        public override IBook? GetBook(int id)
        {
            var bookDB = (from book in _context.BookDB
                          where book.id == id
                          select book).FirstOrDefault();
            if (bookDB == null)
            {
                return null;
            }
            else
            {
                return EntryToObj(bookDB);
            }
        }

        public override IEnumerable<IBook> GetAllBooks()
        {
            var books = from book in _context.BookDB
                        select EntryToObj(book);
            return books;
        }

        // ------------------ Reader ------------------

        private IUser? EntryToObj(ReaderDB readerDB)
        {
            if (readerDB != null)
            {
                return new Reader(readerDB.id, readerDB.name, readerDB.surname, readerDB.email, readerDB.phoneNumber, readerDB.role, readerDB.debt ?? 0);
            }
            return null;
        }

        public override void AddReader(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt)
        {
            ReaderDB readerDB = new ReaderDB
            {
                id = id,
                name = name,
                surname = surname,
                email = email,
                phoneNumber = phoneNumber,
                role = role,
                debt = debt
            };
            _context.ReaderDB.InsertOnSubmit(readerDB);
            _context.SubmitChanges();
        }

        public override void RemoveReader(int id)
        {
            ReaderDB readerDB = _context.ReaderDB.Single(r => r.id == id);
            _context.ReaderDB.DeleteOnSubmit(readerDB);
            _context.SubmitChanges();
        }

        public override IUser? GetReader(int id)
        {
            var readerDB = (from reader in _context.ReaderDB
                            where reader.id == id
                            select reader).FirstOrDefault();
            if (readerDB == null)
            {
                return null;
            }
            else
            {
                return EntryToObj(readerDB);
            }
        }

        public override IEnumerable<IUser> GetAllReaders()
        {
            var readers = from reader in _context.ReaderDB
                          select EntryToObj(reader);
            return readers;
        }

        // ------------------ State ------------------

        private IState? EntryToObj(StateDB stateDB)
        {
            if (stateDB != null)
            {
                return new State(stateDB.stateId, stateDB.quantity ?? 0, stateDB.bookId ?? 0);
            }
            return null;
        }

        public override void AddState(int id, int quantity, int bookId)
        {
            StateDB stateDB = new StateDB
            {
                stateId = id,
                quantity = quantity,
                bookId = bookId
            };
            _context.StateDB.InsertOnSubmit(stateDB);
            _context.SubmitChanges();
        }

        public override void RemoveState(int id)
        {
            StateDB stateDB = _context.StateDB.Single(s => s.stateId == id);
            _context.StateDB.DeleteOnSubmit(stateDB);
            _context.SubmitChanges();
        }

        public override IState? GetState(int id)
        {
            var stateDB = _context.StateDB.SingleOrDefault(s => s.stateId == id);
            if (stateDB == null)
            {
                return null;
            }
            else
            {
                return EntryToObj(stateDB);
            }
        }

        public override IEnumerable<IState> GetAllStates()
        {
            var states = from state in _context.StateDB
                         select EntryToObj(state);
            return states;
        }

        // ------------------ Event ------------------

        private IEvent? EntryToObj(EventDB eventDB)
        {
            if (eventDB != null)
            {
                return new Event(eventDB.eventId, eventDB.userId ?? 0, eventDB.bookId ?? 0);
            }
            return null;
        }

        public override void AddEvent(int id, int userId, int bookId)
        {
            EventDB eventDB = new EventDB
            {
                eventId = id,
                userId = userId,
                bookId = bookId
            };
            _context.EventDB.InsertOnSubmit(eventDB);
            _context.SubmitChanges();
        }

        public override void RemoveEvent(int id)
        {
            EventDB eventDB = _context.EventDB.SingleOrDefault(e => e.eventId == id);
            if (eventDB != null)
            {
                _context.EventDB.DeleteOnSubmit(eventDB);
                _context.SubmitChanges();
            }
        }

        public override IEnumerable<IEvent> GetAllEvents()
        {
            var events = from ev in _context.EventDB
                         select EntryToObj(ev);
            return events;
        }

        // ------------------ Utility ------------------

        public override void ChangeQuantity(int stateId, int change)
        {
            StateDB stateDB = _context.StateDB.Single(s => s.stateId == stateId);
            stateDB.quantity += change;
            _context.SubmitChanges();
        }

        public override void ClearAll()
        {
            _context.ExecuteCommand("DELETE FROM EventDB");
            _context.ExecuteCommand("DELETE FROM StateDB");
            _context.ExecuteCommand("DELETE FROM ReaderDB");
            _context.ExecuteCommand("DELETE FROM BookDB");
        }
    }
}
