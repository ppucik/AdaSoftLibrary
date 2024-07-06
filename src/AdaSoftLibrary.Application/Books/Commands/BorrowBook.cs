using AdaSoftLibrary.Application.Books.Validation;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Exceptions;
using AdaSoftLibrary.Domain.Common;
using AdaSoftLibrary.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Commands;

/// <summary>
/// Vypožičanie knihy
/// </summary>
public class BorrowBook
{
    public class Command : IRequest<Response>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
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
            var validator = new BorrowBookCommandValidator(_bookRepository);
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
                book.Borrowed = new Borrowed
                {
                    FirstName = command.FirstName.Trim(),
                    LastName = command.LastName.Trim(),
                    FromDate = DateOnly.FromDateTime(DateTime.Now)
                };

                await _bookRepository.UpdateAsync(book, cancellationToken);

                response.Data = book;
            }

            return response;
        }
    }
}
