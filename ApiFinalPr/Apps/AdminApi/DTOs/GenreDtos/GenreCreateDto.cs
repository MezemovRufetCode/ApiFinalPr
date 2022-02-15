using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.AdminApi.DTOs.GenreDtos
{
    public class GenreCreateDto
    {
        public string Name { get; set; }
    }
    public class GenreCreateDtoValidator : AbstractValidator<GenreCreateDto>
    {
        public GenreCreateDtoValidator()
        {
            RuleFor(g => g.Name).NotNull().WithMessage("Include genre name").MaximumLength(20).WithMessage("Max length can be 20");
        }
    }
}
