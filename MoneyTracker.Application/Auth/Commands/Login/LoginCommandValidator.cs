using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() 
        { 
            RuleFor(x => x.loginDto)
                .NotNull()
                .WithMessage("Login data must be provided.");
            RuleFor(x => x.loginDto.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");
            RuleFor(x => x.loginDto.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }

    }
}
