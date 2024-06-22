using AdaSoftLibrary.Application.Books.Validation;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Exceptions;
using AdaSoftLibrary.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Commands;

/// <summary>
/// Vrátenie knihy
/// </summary>
public class ReturnBook
{
    public class Command : IRequest<Unit>
    {
        public Command(int id) { Id = id; }
        public int Id { get; set; }
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
            var validator = new ReturnBookCommandValidator(book.IsBorrowed);
            var validationResult = await validator.ValidateAsync(command);
            if (validationResult.Errors.Any())
                throw new ValidationException(validationResult);

            // Persistencia
            book.Borrowed = null;

            await _bookRepository.UpdateAsync(book, cancellationToken);

            return Unit.Value;
        }
    }
}
