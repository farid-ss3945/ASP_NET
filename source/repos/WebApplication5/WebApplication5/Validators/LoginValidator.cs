using FluentValidation;
using WebApplication5.DTOs.User;

namespace WebApplication5.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator() {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
