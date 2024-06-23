using AdaSoftLibrary.Application.Books.Commands;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class BorrowBookCommandValidator : AbstractValidator<BorrowBook.Command>
{
    private readonly bool? _isBorrowed;

    public BorrowBookCommandValidator(bool? isBorrowed = null)
    {
        _isBorrowed = isBorrowed;

        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("{PropertyName} je povinné.")
            .GreaterThan(0).WithMessage("{PropertyName} musí byť väčšie ako 0.")
            .Must(BeNotBorrowed).WithMessage("Kniha je už požičaná !")
            ;

        RuleFor(b => b.FirstName)
            .NotEmpty().WithMessage("{PropertyName} je povinné.")
            .NotNull()
            .MinimumLength(3).WithMessage("{PropertyName} musí mať minimálne 3 znaky.")
            .MaximumLength(100).WithMessage("{PropertyName} nesmie presiahnuť 100 znakov.")
            ;

        RuleFor(b => b.LastName)
            .NotEmpty().WithMessage("{PropertyName} je povinné.")
            .NotNull()
            .MinimumLength(3).WithMessage("{PropertyName} musí mať minimálne 3 znaky.")
            .MaximumLength(100).WithMessage("{PropertyName} nesmie presiahnuť 100 znakov.")
            ;
    }

    // kniha musí byť NEOBJEDNANÁ
    private bool BeNotBorrowed(int id)
    {
        return _isBorrowed == false;
    }
}
