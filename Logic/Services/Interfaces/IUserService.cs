using Data.Users;

namespace Logic.Services.Interfaces
{
    public interface IUserService
    {
        bool AddUser(User user);
        bool RemoveUser(Guid id);
        User GetUser(Guid id);
        List<User> GetAllUsers();
        User CreateReader(string name, string surname, string email, string phoneNumber);
        bool RegisterReader(string name, string surname, string email, string phoneNumber);
    }

}
