using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.DTOs.AuthorDtos
{
    public class AuthorCreateDto
    {
        [Required(ErrorMessage ="You should include author's name")]
        [StringLength(maximumLength:20)]
        public string Name { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
