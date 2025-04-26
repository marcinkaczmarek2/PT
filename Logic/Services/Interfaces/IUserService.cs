using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Users;

namespace Logic.Services.Interfaces
{
    public interface IUserService
    {
        bool AddUser(User user);
        bool RemoveUser(Guid id);
        User GetUser(Guid id);
        List<User> GetAllUsers();
        Reader CreateReader(string name, string surname, string email, string phoneNumber);
        bool RegisterReader(string name, string surname, string email, string phoneNumber);
    }

}
