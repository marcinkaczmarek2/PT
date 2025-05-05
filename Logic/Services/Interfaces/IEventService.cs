using Logic.Models;
using System;
using System.Collections.Generic;

namespace Logic.Services.Interfaces
{
    public interface IEventService
    {
        bool AddEvent(IEventL eventBase);
        List<IEventL> GetAllEvents();
    }
}
