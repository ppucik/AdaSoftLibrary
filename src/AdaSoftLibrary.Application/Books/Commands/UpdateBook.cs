using AdaSoftLibrary.Application.Books.Validation;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Exceptions;
using AdaSoftLibrary.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Commands;

/// <summary>
/// Editácia údajov o knihe
/// </summary>
public class UpdateBook
{
    public class Command : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Author { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? Year { get; set; }
        public string? Description { get; set; }
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
            var validator = new UpdateBookCommandValidator();
            var validationResult = await validator.ValidateAsync(command);
            if (validationResult.Errors.Any())
                throw new ValidationException(validationResult);

            // Persistencia
            _mapper.Map(command, book, typeof(Command), typeof(Book));

            await _bookRepository.UpdateAsync(book, cancellationToken);

            return Unit.Value;
        }
    }
}
