using Presentation.Model.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Model.API
{
    public abstract class IModel
    {
        public abstract List<IBookModelData> GetAllBooks();
        public abstract Task RemoveBook(int id);
        public abstract Task AddBook(int id, string title, string publisher, string author, int numberOfPages, string genre);

        public abstract List<IReaderModelData> GetAllUsers();
        public abstract Task RemoveUser(int id);
        public abstract Task AddUser(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt);

        public abstract List<IStateModelData> GetAllStates();
        public abstract Task RemoveState(int id);
        public abstract Task AddState(int id, int bookId, int quantity);

        public abstract Task RemoveEvent(int id);
        public abstract List<IEventModelData> GetAllEvents();


        public abstract Task AddEvent(int eventId, int userId, int bookId);

        public abstract Task BorrowBook(int eventId, int userId, int bookId);

        public abstract Task ReturnBook(int eventId, int userId, int bookId);

        public static IModel CreateNewModel()
        {
            return new ModelDefault();
        }
    }
}
