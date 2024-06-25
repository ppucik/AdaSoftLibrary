using AdaSoftLibrary.Application.Books.Commands;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBook.Command>
{
    public UpdateBookCommandValidator(bool? isBorrowed = null)
    {
        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("{PropertyName} je povinné.")
            .GreaterThan(0).WithMessage("{PropertyName} musí byť väčšie ako 0.")
            ;

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

        if (isBorrowed == true)
        {
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

            RuleFor(b => b.BorrowedFrom)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("{PropertyName} nemôže byť v budúcnosti.")
                ;
        }
    }
}
