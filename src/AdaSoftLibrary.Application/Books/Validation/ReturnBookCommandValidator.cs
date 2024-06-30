using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Domain.Constants;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class ReturnBookCommandValidator : AbstractValidator<ReturnBook.Command>
{
    private readonly bool? _isBorrowed;

    public ReturnBookCommandValidator(bool? isBorrowed = null)
    {
        _isBorrowed = isBorrowed;

        RuleFor(b => b.Id)
            .NotEmpty().WithMessage(MessageConstants.IdCannotBeEmpty)
            .GreaterThan(0).WithMessage(MessageConstants.IdMustBeGreatherThanZero)
            .Must(BeBorrowed).WithMessage(MessageConstants.BookMustBeBorrowed)
            ;
    }

    // kniha musí byť OBJEDNANÁ
    private bool BeBorrowed(int id)
    {
        return _isBorrowed == true;
    }
}
