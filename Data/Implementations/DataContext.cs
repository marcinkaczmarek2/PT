using Data.API;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations
{
    internal class DataContext : DbContext, IDataContext
    {
        private readonly string? _connectionString;

        public DataContext(string? connectionString = null)
        {
            _connectionString = connectionString;
        }

        // -------------------- Book --------------------
        internal List<IBook> bookData = new();
        public DbSet<Book> _bookDBSet { get; set; }
        public IQueryable<IBook> Book => throw new NotImplementedException();

        public async Task AddBookAsync(IBook bookEntry)
        {
            Book book = new(bookEntry.id, bookEntry.title, bookEntry.publisher, bookEntry.author, bookEntry.numberOfPages, bookEntry.genre);
            await _bookDBSet.AddAsync(book);
            await SaveChangesAsync();
        }

        public async Task RemoveBookAsync(int id)
        {
            Book? book = await _bookDBSet.FindAsync(id);
            if (book != null)
            {
                _bookDBSet.Remove(book);
                await SaveChangesAsync();
            }
        }

        // -------------------- Reader --------------------
        internal List<IUser> readerData = new();
        public DbSet<Reader> _readerDBSet { get; set; }
        public IQueryable<IUser> Reader => _readerDBSet.Select(r =>
            new Reader(r.id, r.name, r.surname, r.email, r.phoneNumber, r.role, r.debt) as IUser);

        public async Task AddReaderAsync(IUser readerEntry)
        {
            Reader reader = new(readerEntry.id, readerEntry.name, readerEntry.surname, readerEntry.email, readerEntry.phoneNumber, readerEntry.role, 0);
            await _readerDBSet.AddAsync(reader);
            await SaveChangesAsync();
        }

        public async Task RemoveReaderAsync(int id)
        {
            Reader? reader = await _readerDBSet.FindAsync(id);
            if (reader != null)
            {
                _readerDBSet.Remove(reader);
                await SaveChangesAsync();
            }
        }


        // -------------------- Event --------------------
        internal List<IEvent> eventData = new();
        public DbSet<Event> _eventDBSet { get; set; }
        public IQueryable<IEvent> Event => throw new NotImplementedException();

        public async Task AddEventAsync(IEvent eventEntry)
        {
            Event e = new(eventEntry.eventId, eventEntry.userId, eventEntry.bookId);
            await _eventDBSet.AddAsync(e);
            await SaveChangesAsync();
        }

        public async Task RemoveEventAsync(int id)
        {
            Event? e = await _eventDBSet.FindAsync(id);
            if (e != null)
            {
                _eventDBSet.Remove(e);
                await SaveChangesAsync();
            }
        }

        // -------------------- State --------------------
        internal List<IState> stateData = new();
        public DbSet<State> _stateDBSet { get; set; }
        public IQueryable<IState> State => throw new NotImplementedException();

        public async Task AddStateAsync(IState stateEntry)
        {
            State s = new(stateEntry.stateId, stateEntry.bookId, stateEntry.quantity);
            await _stateDBSet.AddAsync(s);
            await SaveChangesAsync();
        }

        public async Task RemoveStateAsync(int id)
        {
            State? s = await _stateDBSet.FindAsync(id);
            if (s != null)
            {
                _stateDBSet.Remove(s);
                await SaveChangesAsync();
            }
        }
    }
}
