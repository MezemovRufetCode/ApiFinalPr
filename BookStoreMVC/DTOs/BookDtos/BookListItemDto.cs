using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.DTOs.BookDto
{
    public class BookListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public bool IsDeleted { get; set; }
        public bool DisplayStatus { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
