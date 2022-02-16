using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.UserApi.DTOs.AccountDto
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username).MinimumLength(4).MaximumLength(20).WithMessage("Username max length can be 20").NotNull().WithMessage("Username is required");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Min length must be 8").NotNull().WithMessage("password is required").MaximumLength(25);
        }
    }
}
