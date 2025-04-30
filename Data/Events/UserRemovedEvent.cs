
namespace Data.Events
{
    internal class UserRemovedEvent : EventBase
    {
        internal Guid userId { get; private set; }
        internal string? userEmail { get; private set; }

        internal UserRemovedEvent(Guid userId, string userEmail)
            : base()
        {
            this.userId = userId;
            this.userEmail = userEmail;
        }

        public override string ToString()
        {
            return $"[UserRemovedEvent] UserId: {userId}, Email: {userEmail}, Timestamp: {timestamp}";
        }
    }
}
