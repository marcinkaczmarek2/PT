using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Catalog
{
    public class Borrowable
    {
        public Guid id { private set; get; }
        public string title { set; get; }
        public string publisher { set; get; }
        public bool availbility { set; get; }

        public Borrowable(string title, string publisher, bool availbility)
        {
            id = Guid.NewGuid();
            this.title = title;
            this.publisher = publisher;
            this.availbility = availbility;
        }
    }
    

}
