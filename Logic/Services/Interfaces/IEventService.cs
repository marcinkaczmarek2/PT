using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Events;

namespace Logic.Services.Interfaces
{
    public interface IEventService
    {
        bool AddEvent(EventBase eventBase);
        List<EventBase> GetAllEvents();
    }

}
