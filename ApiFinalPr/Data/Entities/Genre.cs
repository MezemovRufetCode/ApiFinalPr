using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Data.Entities
{
    public class Genre:BaseEntity
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<Book> Books { get; set; }
    }
}
