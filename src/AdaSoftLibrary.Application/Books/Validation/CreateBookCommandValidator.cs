using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Common;
using AdaSoftLibrary.Domain.Constants;
using AdaSoftLibrary.Domain.Enums;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class CreateBookCommandValidator : AbstractValidator<CreateBook.Command>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandValidator(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;

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

        RuleFor(b => b)
            .MustAsync(async (entity, value, ct) => await IsBooklUnique(entity)).WithMessage(MessageConstants.AuthorAndNameMustBeUnique)
            ;
    }

    private async Task<bool> IsBooklUnique(CreateBook.Command command)
    {
        var bookFilter = new BookFilter
        {
            BookStatus = BookStatusEnum.AllBooks,
            Author = command.Author,
            Name = command.Name
        };

        return (await _bookRepository.GetListAsync(bookFilter, new Pagination())).Count() == 0;
    }
}
