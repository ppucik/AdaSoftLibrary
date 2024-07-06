using AdaSoftLibrary.Application.Books.Validation;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Exceptions;
using AdaSoftLibrary.Domain.Common;
using AdaSoftLibrary.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Commands;

/// <summary>
/// Editácia údajov o knihe
/// </summary>
public class UpdateBook
{
    public class Command : IRequest<Response>
    {
        public int Id { get; set; }
        public string Author { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? Year { get; set; }
        public string? Description { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? BorrowedFrom { get; set; }
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
            var validator = new UpdateBookCommandValidator(book.IsBorrowed);
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
                _mapper.Map(command, book, typeof(Command), typeof(Book));

                if (book.Borrowed is not null)
                {
                    book.Borrowed.FirstName = command.FirstName!.Trim();
                    book.Borrowed.LastName = command.LastName!.Trim();
                    book.Borrowed.FromDate = command.BorrowedFrom;
                }

                await _bookRepository.UpdateAsync(book, cancellationToken);

                response.Data = book;
            }

            return response;
        }
    }
}
