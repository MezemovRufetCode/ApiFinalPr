using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Data.Entities
{
    public class Author:BaseEntity
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public List<Book> Books { get; set; }
    }
}
