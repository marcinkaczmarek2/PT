using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Users
{
    public class Employee : User
    {
        public double salary { set; get; }

        public Employee(string name, string surname, string email, string phoneNumber, UserRole role, double salary)
            : base(name, surname, email, phoneNumber, role)
        {
            this.salary = salary;
        }
    }
}
