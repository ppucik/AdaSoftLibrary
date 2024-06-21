using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Exceptions;
using AdaSoftLibrary.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Commands;

/// <summary>
/// Zmazanie knihy
/// </summary>
public class DeleteBook
{
    public class Command : IRequest<Unit>
    {
        public required int Id { get; set; }
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

            // Persistencia
            await _bookRepository.DeleteAsync(book.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
