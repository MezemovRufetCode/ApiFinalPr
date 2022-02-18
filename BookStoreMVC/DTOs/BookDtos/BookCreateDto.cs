using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.DTOs.BookDtos
{
    public class BookCreateDto
    {
        [Required(ErrorMessage ="This field can not be empty")]
        [StringLength(maximumLength:20)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public decimal Cost { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public int GenreId { get; set; }
        public bool IsDeleted { get; set; }
        public bool DisplayStatus { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
