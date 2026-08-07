using CommerceFabric.Core.DTOs;
using FluentValidation;

namespace CommerceFabric.Core.Validators
{
    /// <summary>
    /// As it is an AbstractValidator, it will be injected into the DI container automatically by the AddValidatorsFromAssemblyContaining method in the DependencyInjection class
    /// Whenever a LoginRequest is passed to a controller action, this validator will be used to validate the request before the action is executed.
    /// </summary>
    public class UpdateUserDetailsRequestValidator : AbstractValidator<UpdateUserDetailsRequest>
    {
        public UpdateUserDetailsRequestValidator() 
        {
            // Validate that the UserID property is not empty
            RuleFor(x => x.UserID)
                .NotEmpty().WithMessage("UserID is required.");
        }
    }
}
