using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Constants;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class ReturnBookCommandValidator : AbstractValidator<ReturnBook.Command>
{
    private readonly IBookRepository _bookRepository;

    public ReturnBookCommandValidator(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;

        RuleFor(b => b.Id)
            .NotEmpty().WithMessage(MessageConstants.IdCannotBeEmpty)
            .GreaterThan(0).WithMessage(MessageConstants.IdMustBeGreatherThanZero)
            .MustAsync(async (entity, value, ct) => await BeBorrowed(value)).WithMessage(MessageConstants.BookMustBeBorrowed)
            ;
    }

    // kniha musí byť OBJEDNANÁ
    private async Task<bool> BeBorrowed(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return book?.IsBorrowed == true;
    }
}
