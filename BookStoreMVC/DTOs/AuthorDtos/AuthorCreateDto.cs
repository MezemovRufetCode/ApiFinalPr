using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.DTOs.AuthorDtos
{
    public class AuthorCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
