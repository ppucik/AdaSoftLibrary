using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Domain.Constants;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class CreateBookCommandValidator : AbstractValidator<CreateBook.Command>
{
    //private readonly IBookRepository _bookRepository;

    public CreateBookCommandValidator()
    {
        //_bookRepository = bookRepository;

        RuleFor(b => b.Author)
            .NotEmpty().WithMessage(MessageConstants.AuthorCannotBeEmpty)
            .NotNull()
            .MaximumLength(15).WithMessage(MessageConstants.AuthorCannotExceed15Char)
            ;

        RuleFor(b => b.Name)
            .NotEmpty().WithMessage(MessageConstants.NameCannotBeEmpty)
            .NotNull()
            .MaximumLength(15).WithMessage(MessageConstants.NameCannotExceed15Char)
            ;

        RuleFor(b => b.Year)
            .ExclusiveBetween(1900, 2100).WithMessage(MessageConstants.YearOutOfRange)
            ;
    }

    //private async Task<bool> BookUnique(CreateBook.Command command, CancellationToken token)
    //{
    //    return !(await _bookRepository.GetListAsync);
    //}
}
