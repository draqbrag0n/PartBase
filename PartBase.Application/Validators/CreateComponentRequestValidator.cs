using FluentValidation;
using PartBase.Application.DTOs.Components;

namespace PartBase.Application.Validators;

public class CreateComponentRequestValidator
    : AbstractValidator<CreateComponentRequest>
{
    public CreateComponentRequestValidator()
    {
        RuleFor(x => x.PartNumber)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Package)
            .NotEmpty();

        RuleFor(x => x.ManufacturerId)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}