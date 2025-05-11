using System;
using System.Collections.Generic;
using Data.API.Models;

namespace Logic.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(IUser user);
        IUser? GetUser(Guid id);
        List<IUser> GetAllUsers();
        bool RemoveUser(Guid id);
    }
}
