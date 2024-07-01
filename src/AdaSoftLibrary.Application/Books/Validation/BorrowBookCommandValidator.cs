using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Domain.Constants;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class BorrowBookCommandValidator : AbstractValidator<BorrowBook.Command>
{
    private readonly bool? _isBorrowed;

    public BorrowBookCommandValidator(bool? isBorrowed = null)
    {
        _isBorrowed = isBorrowed;

        RuleFor(b => b.Id)
            .NotEmpty().WithMessage(MessageConstants.IdCannotBeEmpty)
            .GreaterThan(0).WithMessage(MessageConstants.IdMustBeGreatherThanZero)
            .Must(BeNotBorrowed).WithMessage(MessageConstants.BookCannotBeBorrowed)
            ;

        RuleFor(b => b.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(MessageConstants.FirstNameCannotBeEmpty)
            .NotNull()
            .Length(3, 100).WithMessage(MessageConstants.FirstNameOutOfRange)
            ;
        ;

        RuleFor(b => b.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(MessageConstants.LastNameCannotBeEmpty)
            .NotNull()
            .Length(3, 100).WithMessage(MessageConstants.LastNameOutOfRange)
            ;
        ;
    }

    // kniha musí byť NEOBJEDNANÁ
    private bool BeNotBorrowed(int id)
    {
        return _isBorrowed == false;
    }
}
