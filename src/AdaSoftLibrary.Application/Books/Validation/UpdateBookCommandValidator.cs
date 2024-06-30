using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Domain.Constants;
using FluentValidation;

namespace AdaSoftLibrary.Application.Books.Validation;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBook.Command>
{
    public UpdateBookCommandValidator(bool? isBorrowed = null)
    {
        RuleFor(b => b.Id)
            .NotEmpty().WithMessage(MessageConstants.IdCannotBeEmpty)
            .GreaterThan(0).WithMessage(MessageConstants.IdMustBeGreatherThanZero)
            ;

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

        if (isBorrowed == true)
        {
            RuleFor(b => b.FirstName)
                .NotEmpty().WithMessage(MessageConstants.FirstNameCannotBeEmpty)
                .NotNull()
                .Length(3, 100).WithMessage(MessageConstants.FirstNameOutOfRange)
                ;

            RuleFor(b => b.LastName)
                .NotEmpty().WithMessage(MessageConstants.LastNameCannotBeEmpty)
                .NotNull()
                .Length(3, 100).WithMessage(MessageConstants.LastNameOutOfRange)
                ;

            RuleFor(b => b.BorrowedFrom)
                .NotEmpty().WithMessage(MessageConstants.FromDateCannotBeEmpty)
                .NotNull()
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage(MessageConstants.FromDateCurrentOrInPast)
                ;
        }
    }
}
