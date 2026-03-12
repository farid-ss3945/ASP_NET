using FluentValidation;
using WebApplication5.DTOs.User;

namespace WebApplication5.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator() {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required");
        }
    }
}
