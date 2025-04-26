using Data.Users;

namespace Logic.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        bool RemoveUser(Guid id);
        User? GetUser(Guid id);
        List<User> GetAllUsers();
    }
}
