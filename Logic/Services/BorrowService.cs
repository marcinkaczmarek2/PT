using Data.Events;
using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IUserRepository userRepository;
        private readonly ILibraryRepository libraryRepository;
        private readonly IEventService eventService;

        public BorrowService(IUserRepository userRepository, ILibraryRepository libraryRepository, IEventService eventService)
        {
            this.userRepository = userRepository;
            this.libraryRepository = libraryRepository;
            this.eventService = eventService;
        }

        public bool BorrowItem(Guid userId, Guid itemId)
        {
            var user = userRepository.GetUser(userId);
            if (user == null)
            {
                throw new Exception("Error, user does not exist.");
            }

            var item = libraryRepository.GetContent(itemId);
            if (item == null)
            {
                throw new Exception("Error, item does not exist.");
            }

            if (!item.availbility)
            {
                throw new Exception("Error, item is already borrowed.");
            }

            item.availbility = false;
            eventService.AddEvent(new ItemBorrowedEvent(userId, itemId, item.title));
            return true;
        }

        public bool ReturnItem(Guid userId, Guid itemId)
        {
            var user = userRepository.GetUser(userId);
            if (user == null)
            {
                throw new Exception("Error, user does not exist.");
            }

            var item = libraryRepository.GetContent(itemId);
            if (item == null)
            {
                throw new Exception("Error, item does not exist.");
            }

            if (item.availbility)
            {
                throw new Exception("Error, item is not currently borrowed.");
            }

            item.availbility = true;
            eventService.AddEvent(new ItemReturnedEvent(userId, itemId, item.title));
            return true;
        }
    }
}
