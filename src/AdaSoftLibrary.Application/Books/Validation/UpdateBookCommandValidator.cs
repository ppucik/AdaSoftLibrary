using AdaSoftLibrary.Application.Books.Commands;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBook.Command>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("{PropertyName} je požadované.")
            .GreaterThan(0).WithMessage("{PropertyName} musí byť väčšie ako 0.")
            ;

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
