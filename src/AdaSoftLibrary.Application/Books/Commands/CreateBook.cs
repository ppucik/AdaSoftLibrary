﻿using AdaSoftLibrary.Application.Books.Validation;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Common;
using AdaSoftLibrary.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Commands;

/// <summary>
/// Založenie novej knihy
/// </summary>
public class CreateBook
{
    public class Command : IRequest<Response>
    {
        public string Author { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? Year { get; set; }
        public string? Description { get; set; }
    }

    public class Response : BaseResponse<Book?>
    {
        public Response() : base() { }

        public Book? Book { get; set; }
    }

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

            // Validácia 
            var validator = new CreateBookCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (validationResult.Errors.Any())
            {
                //throw new ValidationException(validationResult);
                response.Success = false;
                response.ValidationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            }

            // Persistencia
            if (response.Success)
            {
                response.Book = _mapper.Map<Book>(command);

                await _bookRepository.AddAsync(response.Book, cancellationToken);
            }

            return response;
        }
    }
}