using Logic.Repositories.Interfaces;
using Data.API.Models;
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

        public void AddEvent(IEventD eventBase)
        {
            context.AddEvent(eventBase);
        }

        public List<IEventD> GetAllEvents()
        {
            return context.GetEvents();
        }
    }
}
