using System;
using Logic.Models;
using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    internal sealed class BorrowService : IBorrowService
    {
        private readonly IUserRepository userRepository;
        private readonly ILibraryRepository libraryRepository;
        private readonly IEventService eventService;
        private readonly IEventFactory eventFactory;

        internal BorrowService(
            IUserRepository userRepository,
            ILibraryRepository libraryRepository,
            IEventService eventService,
            IEventFactory eventFactory)
        {
            this.userRepository = userRepository;
            this.libraryRepository = libraryRepository;
            this.eventService = eventService;
            this.eventFactory = eventFactory;
        }

        public bool BorrowItem(Guid userId, Guid itemId)
        {
            var user = userRepository.GetUser(userId);
            if (user == null)
                throw new InvalidOperationException("Error, user does not exist.");

            var item = libraryRepository.GetContent(itemId);
            if (item == null)
                throw new InvalidOperationException("Error, item does not exist.");

            if (!item.availability)
                throw new InvalidOperationException("Error, item is already borrowed.");

            item.availability = false;

            eventService.AddEvent(eventFactory.CreateItemBorrowedEvent(userId, itemId, item.Title));
            return true;
        }

        public bool ReturnItem(Guid userId, Guid itemId)
        {
            var user = userRepository.GetUser(userId);
            if (user == null)
                throw new InvalidOperationException("Error, user does not exist.");

            var item = libraryRepository.GetContent(itemId);
            if (item == null)
                throw new InvalidOperationException("Error, item does not exist.");

            if (item.availability)
                throw new InvalidOperationException("Error, item is not currently borrowed.");

            item.availability = true;

            eventService.AddEvent(eventFactory.CreateItemReturnedEvent(userId, itemId, item.Title));
            return true;
        }
    }
}
