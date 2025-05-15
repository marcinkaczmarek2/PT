using Data.API.Models;
using Logic.Repositories.Interfaces;

namespace Logic.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly List<IUser> users = new();

        public void AddUser(IUser user)
        {
            users.Add(user);
        }

        public bool RemoveUser(Guid id)
        {
            return users.RemoveAll(u => u.id == id) > 0;
        }

        public IUser? GetUser(Guid id)
        {
            return users.FirstOrDefault(u => u.id == id);
        }

        public List<IUser> GetAllUsers()
        {
            return new List<IUser>(users);
        }
    }
}
