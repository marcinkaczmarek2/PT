using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Models;
using Logic.Repositories.Interfaces;

namespace Logic.Repositories
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
            return users.RemoveAll(u => u.Id == id) > 0;
        }

        public IUser? GetUser(Guid id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public List<IUser> GetAllUsers()
        {
            return new List<IUser>(users);
        }
    }
}
