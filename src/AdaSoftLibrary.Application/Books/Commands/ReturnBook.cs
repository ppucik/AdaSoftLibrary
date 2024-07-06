using AdaSoftLibrary.Application.Books.Validation;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Exceptions;
using AdaSoftLibrary.Domain.Common;
using AdaSoftLibrary.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Commands;

/// <summary>
/// Vrátenie knihy
/// </summary>
public class ReturnBook
{
    public class Command : IRequest<Response>
    {
        public Command(int id) { Id = id; }
        public int Id { get; set; }
    }

    public class Response : BaseResponse<Book?> { }

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public CommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
        {
            var response = new Response();

            // Kontrola existencie
            var book = await _bookRepository.GetByIdAsync(command.Id, cancellationToken);
            if (book is null)
                throw new NotFoundException(nameof(Book), command.Id);

            // Validácia údajov
            var validator = new ReturnBookCommandValidator(_bookRepository);
            var validationResult = await validator.ValidateAsync(command);

            if (validationResult.Errors.Any())
            {
                response.Message = "Validačné chyby";
                response.ValidationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                response.Success = false;
            }

            // Persistencia
            if (response.Success)
            {
                book.Borrowed = null;

                await _bookRepository.UpdateAsync(book, cancellationToken);

                response.Data = book;
            }

            return response;
        }
    }
}
