using AdaSoftLibrary.Api.Contracts;
using AdaSoftLibrary.Api.Contractsô;
using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Books.Contracts;
using AdaSoftLibrary.Application.Books.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdaSoftLibrary.Api.Endpoints;

public class BookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/books").WithOpenApi();

        api.MapGet("/", GetBooks)
            .Produces<IReadOnlyCollection<GetBookResponse>>(StatusCodes.Status200OK)
            .WithSummary("Zoznam kníh");

        api.MapGet("/{id:int}", GetBook)
            .Produces<GetBookResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Detail knihy");

        api.MapPost("/", CreateBook)
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Zaevidovanie novej knihy");

        api.MapPut("/{id:int}", UpdateBook)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Editácia údajov o knihe");

        api.MapPost("/{id:int}/borrow", BorrowBook)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Vypožičanie knihy");

        api.MapPost("/{id:int}/return", ReturnBook)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Vrátenie knihy");

        api.MapDelete("/{id:int}", DeleteBook)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .WithSummary("Vymazanie knihy");
    }

    private async Task<IResult> GetBooks([AsParameters] GetBooksRequest request, IMediator mediator)
    {
        var query = new GetBooks.Query
        {
            BookFilter = request.BookFilter,
            SearchTerm = request.SearchTerm
        };

        var result = await mediator.Send(query);

        return TypedResults.Ok(result);
    }

    private async Task<IResult> GetBook([FromRoute] int id, IMediator mediator)
    {
        var query = new GetBook.Query(id);
        var result = await mediator.Send(query);

        return result is not null
            ? TypedResults.Ok(result)
            : TypedResults.NotFound();
    }

    private async Task<IResult> CreateBook([FromBody] CreateBookRequest request, IMediator mediator)
    {
        var command = new CreateBook.Command
        {
            Author = request.Author,
            Name = request.Name,
            Year = request.Year,
            Description = request.Description
        };

        var result = await mediator.Send(command);

        return result.Success
            ? TypedResults.Created($"/api/books/{result}", result)
            : TypedResults.BadRequest(result.ValidationErrorsSummary);
    }

    private async Task<IResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookRequest request, IMediator mediator)
    {
        var command = new UpdateBook.Command
        {
            Id = id,
            Author = request.Author,
            Name = request.Name,
            Year = request.Year,
            Description = request.Description
        };

        var result = await mediator.Send(command);

        return result.Success
            ? TypedResults.Ok()
            : TypedResults.BadRequest(result.ValidationErrorsSummary);
    }

    private async Task<IResult> BorrowBook([FromRoute] int id, [FromBody] BorrowBookRequest request, IMediator mediator)
    {
        var command = new BorrowBook.Command
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await mediator.Send(command);

        return TypedResults.Ok();
    }

    private async Task<IResult> ReturnBook([FromRoute] int id, IMediator mediator)
    {
        var command = new ReturnBook.Command(id);
        var result = await mediator.Send(command);

        return TypedResults.Ok();
    }

    private async Task<IResult> DeleteBook([FromRoute] int id, IMediator mediator)
    {
        var command = new DeleteBook.Command(id);
        var result = await mediator.Send(command);

        return TypedResults.NoContent();
    }
}
