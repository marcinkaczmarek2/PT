using System;

namespace Logic.Models
{
    public interface IUser
    {
        Guid Id { get; }
        string Name { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        UserRole Role { get; set; }
    }
}
