using AdaSoftLibrary.Application.Books.Commands;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class ReturnBookCommandValidator : AbstractValidator<ReturnBook.Command>
{
    private readonly bool? _isBorrowed;

    public ReturnBookCommandValidator(bool? isBorrowed = null)
    {
        _isBorrowed = isBorrowed;

        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("{PropertyName} je povinné.")
            .GreaterThan(0).WithMessage("{PropertyName} musí byť väčšie ako 0.")
            .Must(BeBorrowed).WithMessage("Kniha nie je požičaná !")
            ;
    }

    // kniha musí byť OBJEDNANÁ
    private bool BeBorrowed(int id)
    {
        return _isBorrowed == true;
    }
}
