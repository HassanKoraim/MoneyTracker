using FluentValidation;
using MoneyTracker.Application.Categories.Commands.CreateCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.registerDto)
                .NotNull()
                .WithMessage("Register data must be provided.");
            RuleFor(r => r.registerDto.Role).IsInEnum().WithMessage("Role is incorrect");
            RuleFor(x => x.registerDto.FullName)
                  .NotEmpty()
                  .WithMessage("Full name is required.")
                  .Length(2, 100)
                  .WithMessage("Full name must be between 2 and 100 characters.");
            RuleFor(x => x.registerDto.Email)
                  .NotEmpty().WithMessage("Email is required.")
                  .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.registerDto.Password).NotEmpty().WithMessage("Password is Required.");
            RuleFor(x => x.registerDto.Gender)
                .IsInEnum().
                WithMessage("Type is incorrect.");
            RuleFor(x => x.registerDto.PhoneNumber).NotEmpty()
                .WithMessage("Phone Number is required")
                .Matches(@"^(010|011|012|015)[0-9]{9}$")
                .WithMessage("Phone number is incorrect");
            RuleFor(x => x.registerDto.BirthDate).
                LessThan(DateTime.Today).
                WithMessage("Birthdate is incorrect")
                .Must(d=> DateTime.Today.Year - d.Year >= 18)
                .WithMessage("Birthdate must be Greater than or equal 18 Years");
            

        }

    }
}
