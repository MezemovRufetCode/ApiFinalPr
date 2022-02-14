using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Data.Entities
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public bool IsDeleted { get; set; }
        public bool DisplayStatus { get; set; }
    }
}
