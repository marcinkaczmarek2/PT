using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Events
{
    public class UserAddedEvent : EventBase
    {
        public Guid userId { get; set; }
        public string userEmail { get; set; }

        public UserAddedEvent(Guid userId, string userEmail)
            : base()
        {
            this.userId = userId;
            this.userEmail = userEmail;
        }

        public override string ToString()
        {
            return $"[UserAddedEvent] UserId: {userId}, Email: {userEmail}, Timestamp: {timestamp}";
        }
    }
}
