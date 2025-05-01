using Data.API.Models;
using Data.Enums;
using Data.Users;

namespace Data.Factories
{
    internal sealed class UserFactory : IUserFactory
    {
        public IUser CreateReader(string name, string surname, string email, string phoneNumber)
        {
            return new Reader(name, surname, email, phoneNumber, UserRole.Reader, 0.0d);
        }
    }
}
