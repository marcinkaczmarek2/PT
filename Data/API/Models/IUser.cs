using Data.Enums;
namespace Data.API.Models
{
    public interface IUser
    {
        Guid id { get; }
        string name { get; set; }
        string surname { get; set; }
        string email { get; set; }
        string phoneNumber { get; set; }
        UserRole role { get; set; }
    }
}
