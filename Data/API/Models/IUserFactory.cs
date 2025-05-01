namespace Data.API.Models
{
    public interface IUserFactory
    {
        IUser CreateReader(string name, string surname, string email, string phoneNumber);
    }
}