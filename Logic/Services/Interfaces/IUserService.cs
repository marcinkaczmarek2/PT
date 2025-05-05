using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IUserService
    {
        bool AddUser(IUser user);
        bool RemoveUser(Guid id);
        IUser GetUser(Guid id);
        List<IUser> GetAllUsers();
        IUser CreateReader(string name, string surname, string email, string phoneNumber);
        bool RegisterReader(string name, string surname, string email, string phoneNumber);
    }

}
