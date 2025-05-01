using Logic.Repositories.Interfaces;
using Data.API;
using Data.API.Models;
namespace Logic.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly IData context;

        public UserRepository(IData context)
        {
            this.context = context;
        }

        public void AddUser(IUser user)
        {
            context.AddUser(user);
        }

        public bool RemoveUser(Guid id)
        {
            return context.DeleteUser(id);
        }

        public IUser? GetUser(Guid id)
        {
            return context.GetUser(id);
        }

        public List<IUser> GetAllUsers()
        {
            return context.GetUsers();
        }
    }
}
