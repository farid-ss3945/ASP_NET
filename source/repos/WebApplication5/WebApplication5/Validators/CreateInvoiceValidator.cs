using FluentValidation;
using WebApplication5.DTOs.Invoice;

namespace WebApplication5.Validators
{
    // The validator should target the DTO, not itself.
    public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceDto>
    {
        public CreateInvoiceValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("CustomerId must be greater than 0 and match");
        }
    }
}
