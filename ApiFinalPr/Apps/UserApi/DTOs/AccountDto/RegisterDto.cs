using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.UserApi.DTOs.AccountDto
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class RegisterDtoValidaor : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidaor()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("Username is required").MaximumLength(20).MinimumLength(4);
            RuleFor(x => x.Fullname).NotNull().WithMessage("Fullname is required").MaximumLength(20).MinimumLength(4);
            RuleFor(x => x.Password).NotNull().WithMessage("Password can not be null").MaximumLength(20).MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Confirm password can not be null").MaximumLength(20).MinimumLength(8);
            RuleFor(x => x).Custom((x, context)=>{
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure("ConfirmPassword", "Password and Confirm password is not match");
                }
            });
        }
    }
}
