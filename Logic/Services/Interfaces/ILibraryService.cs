using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Catalog;

namespace Logic.Services.Interfaces
{
    public interface ILibraryService
    {
        bool AddContent(Borrowable content);
        bool RemoveContent(Guid id);
        Borrowable? GetContent(Guid id);
        List<Borrowable> GetAllContent();
    }

}
