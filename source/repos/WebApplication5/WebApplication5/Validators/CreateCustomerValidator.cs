using WebApplication5.DTOs.Customer;
using FluentValidation;

namespace WebApplication5.Validators
{
    public class CreateCustomerValidator:AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .GreaterThan(0).WithMessage("UserId must be greater than 0 and match");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
        }
    }
}
