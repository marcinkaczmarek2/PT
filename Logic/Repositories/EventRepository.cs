using Data.Events;
using Data;
using Logic.Repositories.Interfaces;

namespace Logic.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IData context;

        public EventRepository(IData context)
        {
            this.context = context;
        }

        public void AddEvent(EventBase eventBase)
        {
            context.AddEvent(eventBase);
        }

        public List<EventBase> GetAllEvents()
        {
            return context.GetEvents();
        }
    }
}
