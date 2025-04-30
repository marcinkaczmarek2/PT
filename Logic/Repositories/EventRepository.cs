using Data.Events;
using Logic.Repositories.Interfaces;
using Data.API;

namespace Logic.Repositories
{
    internal class EventRepository : IEventRepository
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
