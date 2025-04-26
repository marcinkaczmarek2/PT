using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Catalog;
using Data.Events;
using Data.Users;

namespace Logic.Repositories
{
    public class LibraryRepository
    {
        private readonly IData context;

        public LibraryRepository(IData context)
        {
            this.context = context;
        }

        public void AddContent(Borrowable content)
        {
            this.context.AddItem(content);
        }

        public bool RemoveContent(Guid id)
        {
            return this.context.DeleteItem(id);
        }

        public Borrowable? GetContent(Guid id)
        {
            return this.context.GetItem(id);
        }

        public List<Borrowable> GetAllContent()
        {
            return this.context.GetItems();
        }
    }
}
