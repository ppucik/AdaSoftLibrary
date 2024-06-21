using AdaSoftLibrary.Application.Books.Validation;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Exceptions;
using AdaSoftLibrary.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Commands;

/// <summary>
/// Vypožičanie knihy
/// </summary>
public class BorrowBook
{
    public class Command : IRequest<Unit>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }

    public class CommandHandler : IRequestHandler<Command, Unit>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public CommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
        {
            // Kontrola existencie
            var book = await _bookRepository.GetByIdAsync(command.Id, cancellationToken);
            if (book is null)
                throw new NotFoundException(nameof(Book), command.Id);

            // Validácia údajov
            var validator = new BorrowBookCommandValidator(book.IsBorrowed);
            var validationResult = await validator.ValidateAsync(command);
            if (validationResult.Errors.Any())
                throw new ValidationException(validationResult);

            // Persistencia
            book.Borrowed = new Borrowed
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                FromDate = DateOnly.FromDateTime(DateTime.Now)
            };

            await _bookRepository.UpdateAsync(book, cancellationToken);

            return Unit.Value;
        }
    }
}
