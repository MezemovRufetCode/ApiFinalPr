using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.DTOs.AuthorDtos
{
    public class AuthorListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
