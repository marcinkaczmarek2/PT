using Data.API;
using Services.API;
using Services.Implementation;


namespace Services.Implementation
{
    internal class LibraryService : ILibraryService
    {
        private readonly IDataRepository _repository;

        public LibraryService()
        {
            _repository = IDataRepository.CreateNewRepository();
        }

        public LibraryService(IDataRepository repository)
        {
            _repository = repository;
        }

        // ----------- Book -----------

        private BookServiceData BookToService(IBook b) =>
            new BookServiceData(b.id, b.title, b.publisher, b.author, b.numberOfPages, b.genre);

        public async override Task AddBook(int id, string title, string publisher, string author, int numberOfPages, string genre) =>
            await Task.Run(() => _repository.AddBook(id, title, publisher, author, numberOfPages, genre));

        public async override Task RemoveBook(int id) =>
            await Task.Run(() => _repository.RemoveBook(id));

        public override List<IBookServiceData> GetAllBooks() =>
            _repository.GetAllBooks()
                       .Select(b => (IBookServiceData)BookToService(b))
                       .ToList();

        // ----------- Reader -----------

        private ReaderServiceData ReaderToService(IUser r) =>
            new ReaderServiceData(r.id, r.name, r.surname, r.email, r.phoneNumber, r.role, 0);

        public async override Task AddReader(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt) =>
            await Task.Run(() => _repository.AddReader(id, name, surname, email, phoneNumber, role, debt));

        public async override Task RemoveReader(int id) =>
            await Task.Run(() => _repository.RemoveReader(id));

        public override List<IReaderServiceData> GetAllReaders() =>
            _repository.GetAllReaders()
                       .Select(r => (IReaderServiceData)ReaderToService(r))
                       .ToList();

        // ----------- State -----------

        private StateServiceData StateToService(IState s) =>
            new StateServiceData(s.stateId, s.bookId, s.quantity);

        public async override Task AddState(int stateId, int bookId, int quantity) =>
            await Task.Run(() => _repository.AddState(stateId, bookId, quantity));

        public async override Task RemoveState(int stateId) =>
            await Task.Run(() => _repository.RemoveState(stateId));

        public override List<IStateServiceData> GetAllStates() =>
    _repository.GetAllStates()
               .Select(s => (IStateServiceData)StateToService(s))
               .ToList();

        // ----------- Event -----------

        private EventServiceData EventToService(IEvent e) =>
            new EventServiceData(e.eventId, e.userId, e.bookId, 0);

        public async override Task AddEvent(int eventId, int userId, int bookId) =>
            await Task.Run(() => _repository.AddEvent(eventId, userId, bookId));

        public async override Task RemoveEvent(int eventId) =>
            await Task.Run(() => _repository.RemoveEvent(eventId));

        public override List<IEventServiceData> GetAllEvents() =>
            _repository.GetAllEvents()
                       .Select(e => (IEventServiceData)EventToService(e))
                       .ToList();

        // --- Business Logic ---
        public async override Task BorrowBook(int eventId, int userId, int bookId)
        {
            var state = _repository.GetAllStates().FirstOrDefault(s => s.bookId == bookId);
            if (state == null || state.quantity < 1)
                throw new System.Exception("No copies of the book available to borrow");

            await AddEvent(eventId, userId, bookId);
            _repository.ChangeQuantity(state.stateId, -1);
        }

        public async override Task ReturnBook(int eventId, int userId, int bookId)
        {
            var state = _repository.GetAllStates().FirstOrDefault(s => s.bookId == bookId);
            if (state == null)
                throw new System.Exception("State for the book not found");

            await AddEvent(eventId, userId, bookId);
            _repository.ChangeQuantity(state.stateId, +1);
        }


    }
}
