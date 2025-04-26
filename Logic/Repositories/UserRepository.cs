using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Users;
using Data;
namespace Logic.Repositories
{
    public class UserRepository
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
