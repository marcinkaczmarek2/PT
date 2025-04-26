using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Interfaces
{
    public interface IBorrowService
    {
        bool BorrowItem(Guid userId, Guid itemId);
        bool ReturnItem(Guid userId, Guid itemId);
    }

}
