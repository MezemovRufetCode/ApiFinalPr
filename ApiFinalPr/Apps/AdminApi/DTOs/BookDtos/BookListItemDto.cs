using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.AdminApi.DTOs.BookDtos
{
    public class BookListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string Image { get; set; }
        public bool DisplayStatus { get; set; }
        public AuthorInProductListItemDto Author { get; set; }
        public GenreInProductListItemDto Genre { get; set; }
        //public int AuthorId { get; set; }
        //public int GenreId { get; set; }
    }
    public class AuthorInProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GenreInProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
