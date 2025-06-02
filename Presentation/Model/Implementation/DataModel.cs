using Presentation.Model.API;
using Services.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Model.Implementation
{
    internal class ModelDefault : IModel, IDisposable
    {
        private ILibraryService _service;

        internal ModelDefault(ILibraryService? service = null)
        {
            _service = service ?? ILibraryService.CreateNewService();
        }

        public void Dispose()
        {
            if (_service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        // ----------------- Book -----------------

        private BookModelData? BookToModel(IBookServiceData b)
        {
            return b == null ? null : new BookModelData(b.id, b.title, b.publisher, b.author, b.numberOfPages, b.genre);
        }

        public override List<IBookModelData> GetAllBooks()
        {
            var listData = _service.GetAllBooks();
            List<IBookModelData> result = new();

            foreach (var entry in listData)
            {
                var model = BookToModel(entry);
                if (model != null)
                    result.Add(model);
            }
            return result;
        }

        public override async Task RemoveBook(int id)
        {
            await _service.RemoveBook(id);
        }

        public override async Task AddBook(int id, string title, string publisher, string author, int numberOfPages, string genre)
        {
            await _service.AddBook(id, title, publisher, author, numberOfPages, genre);
        }

        // ----------------- Reader -----------------

        private ReaderModelData? ReaderToModel(IReaderServiceData r)
        {
            return r == null ? null : new ReaderModelData(r.id, r.name, r.surname, r.email, r.phoneNumber, r.role, r.debt);
        }

        public override List<IReaderModelData> GetAllUsers()
        {
            var listData = _service.GetAllReaders();
            List<IReaderModelData> result = new();

            foreach (var entry in listData)
            {
                var model = ReaderToModel(entry);
                if (model != null)
                    result.Add(model);
            }
            return result;
        }

        public override async Task RemoveUser(int id)
        {
            await _service.RemoveReader(id);
        }

        public override async Task AddUser(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt)
        {
            await _service.AddReader(id, name, surname, email, phoneNumber, role, debt);
        }

        // ----------------- State -----------------

        private StateModelData? StateToModel(IStateServiceData s)
        {
            return s == null ? null : new StateModelData(s.stateId, s.bookId, s.quantity);
        }

        public override List<IStateModelData> GetAllStates()
        {
            var listData = _service.GetAllStates();
            List<IStateModelData> result = new();

            foreach (var entry in listData)
            {
                var model = StateToModel(entry);
                if (model != null)
                    result.Add(model);
            }
            return result;
        }

        public override async Task RemoveState(int stateId)
        {
            await _service.RemoveState(stateId);
        }

        public override async Task AddState(int stateId, int bookId, int quantity)
        {
            await _service.AddState(stateId, bookId, quantity);
        }

        // ----------------- Event -----------------

        private EventModelData? EventToModel(IEventServiceData e)
        {
            return e == null ? null : new EventModelData(e.id, e.userId, e.stateId);
        }

        public override List<IEventModelData> GetAllEvents()
        {
            var listData = _service.GetAllEvents();
            List<IEventModelData> result = new();

            foreach (var entry in listData)
            {
                var model = EventToModel(entry);
                if (model != null)
                    result.Add(model);
            }
            return result;
        }

        public override async Task RemoveEvent(int eventId)
        {
            await _service.RemoveEvent(eventId);
        }

        public override async Task AddEvent(int eventId, int userId, int bookId)
        {
            await _service.AddEvent(eventId, userId, bookId);
        }

        // ----------------- Actions -----------------

        public override async Task BorrowBook(int eventId, int userId, int bookId)
        {
            await _service.BorrowBook(eventId, userId, bookId);
        }

        public override async Task ReturnBook(int eventId, int userId, int bookId)
        {
            await _service.ReturnBook(eventId, userId, bookId);
        }
    }
}
