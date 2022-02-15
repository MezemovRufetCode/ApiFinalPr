using EduHomeBEProject.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.AdminApi.DTOs.AuthorDtos
{
    public class AuthorCreateDto
    {
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class AuthorCreateDtoValidator : AbstractValidator<AuthorCreateDto>
    {
        public AuthorCreateDtoValidator()
        {
            RuleFor(a => a.Name).NotNull().WithMessage("Include author name").MaximumLength(20).WithMessage("Max length can be 20");
            RuleFor(a => a).Custom((x, context) =>
            {
                if (x.ImageFile == null)
                {
                    context.AddFailure("ImageFile", "You should include author image");
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
