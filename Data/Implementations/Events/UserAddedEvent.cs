namespace Data.Implementations.Events
{
    internal class UserAddedEvent : EventBase
    {
        internal Guid userId { get; private set; }
        internal string userEmail { get; private set; }

        internal UserAddedEvent(Guid userId, string userEmail)
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
