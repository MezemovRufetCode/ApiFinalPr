using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.DTOs.GenreDtos
{
    public class GenreCreateDto
    {
        [Required(ErrorMessage =("This filed can not be empty"))]
        [StringLength(maximumLength:20)]
        public string Name { get; set; }
    }
}
