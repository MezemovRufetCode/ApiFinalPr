using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.AdminApi.DTOs.BookDtos
{
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public decimal Cost { get; set; }
        public bool IsDeleted { get; set; }
        public bool DisplayStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public AuthorInProductGetDto Author { get; set; }
        public GenreInProductGetDto Genre { get; set; }
    }
    public class AuthorInProductGetDto
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int BooksCount { get; set; }

    }
    public class GenreInProductGetDto
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public int BooksCount { get; set; }
    }
}
