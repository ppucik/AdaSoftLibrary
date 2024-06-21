using AdaSoftLibrary.Application.Books.Commands;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class CreateBookCommandValidator : AbstractValidator<CreateBook.Command>
{
    public CreateBookCommandValidator()
    {
        RuleFor(b => b.Author)
            .NotEmpty().WithMessage("{PropertyName} je požadovaný.")
            .NotNull()
            .MaximumLength(1000).WithMessage("{PropertyName} nesmie presiahnuť 250 znakov.")
            ;

        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("{PropertyName} je požadované.")
            .NotNull()
            .MaximumLength(1000).WithMessage("{PropertyName} nesmie presiahnuť 1000 znakov.")
            ;
    }
}
