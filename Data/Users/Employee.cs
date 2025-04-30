
namespace Data.Users
{
    internal class Employee : User
    {
        internal double salary { private set; get; }

        internal Employee(string name, string surname, string email, string phoneNumber, UserRole role, double salary)
            : base(name, surname, email, phoneNumber, role)
        {
            this.salary = salary;
        }
    }
}
