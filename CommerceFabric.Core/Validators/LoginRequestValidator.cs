using CommerceFabric.Core.DTOs;
using FluentValidation;

namespace CommerceFabric.Core.Validators
{
    /// <summary>
    /// As it is an AbstractValidator, it will be injected into the DI container automatically by the AddValidatorsFromAssemblyContaining method in the DependencyInjection class
    /// Whenever a LoginRequest is passed to a controller action, this validator will be used to validate the request before the action is executed.
    /// </summary>
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator() 
        {
            // Validate that the Email property is not empty and is a valid email address
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            // Validate that the Password property is not empty and has a minimum length of 6 characters
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
