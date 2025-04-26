using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Events;
using Data;

namespace Logic.Repositories
{
    public class EventRepository
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
