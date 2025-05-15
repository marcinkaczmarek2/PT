using Logic.Repositories.Interfaces;
using Data.API.Models;
using Data.API;

namespace Logic.Repositories.Implementations
{
    internal class EventRepository : IEventRepository
    {
        private readonly IData context;

        public EventRepository(IData context)
        {
            this.context = context;
        }

        public void AddEvent(IEvent eventBase)
        {
            context.AddEvent(eventBase);
        }

        public List<IEvent> GetAllEvents()
        {
            return context.GetEvents();
        }
    }
}
