using AdaSoftLibrary.Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Queries;

/// <summary>
/// Vrátí zoznam autorov
/// </summary>
public class GetAuthors
{
    public class Query : IRequest<IReadOnlyCollection<string>>
    {
        public string? SearchAuthor { get; set; }
    }

    public class QueryHandler : IRequestHandler<Query, IReadOnlyCollection<string>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public QueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IReadOnlyCollection<string>> Handle(Query query, CancellationToken cancellationToken)
        {
            var authors = await _bookRepository.GetAuthorsAsync(query.SearchAuthor, cancellationToken);

            return authors.ToList();
        }
    }
}