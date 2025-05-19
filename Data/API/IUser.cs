namespace Data.API
{
    public interface IUser
    {
        int id { get; set; }
        string name { get; set; }
        string surname { get; set; }
        string email { get; set; }
        string phoneNumber { get; set; }
        string role { get; set; }
    }
}
