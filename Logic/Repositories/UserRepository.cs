using Data.Users;
using Logic.Repositories.Interfaces;
using Data.API;
namespace Logic.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly IData context;

        public UserRepository(IData context)
        {
            this.context = context;
        }

        public void AddUser(User user)
        {
            context.AddUser(user);
        }

        public bool RemoveUser(Guid id)
        {
            return context.DeleteUser(id);
        }

        public User? GetUser(Guid id)
        {
            return context.GetUser(id);
        }

        public List<User> GetAllUsers()
        {
            return context.GetUsers();
        }
    }
}
