using AdaSoftLibrary.Application.Books.Commands;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class CreateBookCommandValidator : AbstractValidator<CreateBook.Command>
{
    //private readonly IBookRepository _bookRepository;

    public CreateBookCommandValidator()
    {
        //_bookRepository = bookRepository;

        RuleFor(b => b.Author)
            .NotEmpty().WithMessage("{PropertyName} je povinný.")
            .NotNull()
            .MaximumLength(250).WithMessage("{PropertyName} nesmie presiahnuť 250 znakov.")
            ;

        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("{PropertyName} je povinný.")
            .NotNull()
            .MaximumLength(1000).WithMessage("{PropertyName} nesmie presiahnuť 1000 znakov.")
            ;

        RuleFor(b => b.Year)
            .ExclusiveBetween(0, 9999).WithMessage("{PropertyName} musí byť v rozsahu 0 až 9999.")
            ;
    }

    //private async Task<bool> BookUnique(CreateBook.Command command, CancellationToken token)
    //{
    //    return !(await _bookRepository.GetListAsync);
    //}
}
