using Data.API.Models;
using Data.Implementations.Enums;
using Data.Implementations.Users;

namespace Data.Implementations.Factories
{
    internal sealed class UserFactory : IUserFactory
    {
        public IUser CreateReader(string name, string surname, string email, string phoneNumber)
        {
            return new Reader(name, surname, email, phoneNumber, UserRole.Reader, 0.0d);
        }
    }
}
