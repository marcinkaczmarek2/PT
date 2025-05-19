using Services.API;

namespace Services.Implementation
{
    internal class EventServiceData : IEventServiceData
    {
        public EventServiceData(int id, int stateId, int userId, int quantityChanged)
        {
            this.id = id;
            this.stateId = stateId;
            this.userId = userId;
            this.quantityChanged = quantityChanged;
        }

        public int id { get; }
        public int stateId { get; }
        public int userId { get; }
        public int quantityChanged { get; set; }
    }
}
