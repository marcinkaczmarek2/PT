namespace Data.Users
{
    public abstract class User
    {
        public Guid id { get; private set; }
        public string name { get; set; }
        public string surname{ get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public UserRole role { get; set; }

        public User(string name, string surname, string email, string phoneNumber, UserRole role)
        {
            id = Guid.NewGuid();
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.role = role;
        }
    }

    public enum UserRole
    {
        Admin,
        Reader,
        Guest
    }
}
