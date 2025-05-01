using Data.API.Models;

namespace Logic.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(IUser user);
        bool RemoveUser(Guid id);
        IUser? GetUser(Guid id);
        List<IUser> GetAllUsers();
    }
}
