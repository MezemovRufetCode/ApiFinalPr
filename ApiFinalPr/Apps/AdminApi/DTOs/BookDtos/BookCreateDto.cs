using EduHomeBEProject.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.AdminApi.DTOs.BookDtos
{
    public class BookCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public bool IsDeleted { get; set; }
        public bool DisplayStatus { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
    {
        public BookCreateDtoValidator()
        {
            RuleFor(b => b.AuthorId).NotNull().WithMessage("You should include author id").GreaterThanOrEqualTo(1);
            RuleFor(b => b.GenreId).NotNull().WithMessage("You should include genre id").GreaterThanOrEqualTo(1);
            RuleFor(b => b.Name).NotNull().WithMessage("Name is required").MaximumLength(50).WithMessage("Max length can be 50");
            RuleFor(b => b.Price).NotNull().WithMessage("Price is required").GreaterThanOrEqualTo(0).WithMessage("Price can not be smaller than 0");
            RuleFor(b => b.Cost).NotNull().WithMessage("Cost is required").GreaterThanOrEqualTo(0).WithMessage("Cost can not be smaller than 0");
            RuleFor(b => b.DisplayStatus).NotNull().WithMessage("Display status is rquired");
            RuleFor(b => b).Custom((x, context) =>
            {
                if (x.Price < x.Cost)
                {
                    context.AddFailure("Cost", "Price must be higher than Cost");
                }
                if (x.ImageFile == null)
                {
                    context.AddFailure("ImageFile", "You should include image");
                }
                if (x.ImageFile != null)
                {
                    if (!x.ImageFile.CheckSize(2))
                    {
                        context.AddFailure("ImageFile", "Image size max can be 2mb");
                    }
                    if (!x.ImageFile.IsImage())
                    {
                        context.AddFailure("ImageFile", "You should include only image file");
                    }
                }
            });
        }
    }

}
