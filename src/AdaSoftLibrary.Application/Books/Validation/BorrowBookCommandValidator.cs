using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Constants;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class BorrowBookCommandValidator : AbstractValidator<BorrowBook.Command>
{
    private readonly IBookRepository _bookRepository;

    public BorrowBookCommandValidator(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;

        RuleFor(b => b.Id)
            .NotEmpty().WithMessage(MessageConstants.IdCannotBeEmpty)
            .GreaterThan(0).WithMessage(MessageConstants.IdMustBeGreatherThanZero)
            .MustAsync(async (entity, value, ct) => await BeNotBorrowed(value)).WithMessage(MessageConstants.BookCannotBeBorrowed)
            ;

        RuleFor(b => b.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(MessageConstants.FirstNameCannotBeEmpty)
            .NotNull()
            .Length(3, 100).WithMessage(MessageConstants.FirstNameOutOfRange)
            ;

        RuleFor(b => b.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(MessageConstants.LastNameCannotBeEmpty)
            .NotNull()
            .Length(3, 100).WithMessage(MessageConstants.LastNameOutOfRange)
            ;
    }

    // kniha musí byť NEOBJEDNANÁ
    private async Task<bool> BeNotBorrowed(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return book?.IsBorrowed == false;
    }
}
