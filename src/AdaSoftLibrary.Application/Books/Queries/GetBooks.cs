using AdaSoftLibrary.Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Queries;

/// <summary>
/// Vrátí zoznam kníh
/// </summary>
public class GetBooks
{
    public class Query : IRequest<IReadOnlyCollection<GetBookResponse>>
    {
        /// <summary>
        /// Zapožičané knihy (True/False), všetky knihy (Null)
        /// </summary>
        public bool? Borrowed { get; set; }

        /// <summary>
        /// Časť názvu knihy alebo mena autora
        /// </summary>
        public string? SearchTerm { get; set; }
    }

    public class QueryHandler : IRequestHandler<Query, IReadOnlyCollection<GetBookResponse>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public QueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IReadOnlyCollection<GetBookResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetListAsync(query.SearchTerm, query.Borrowed, cancellationToken);

            return _mapper.Map<IReadOnlyCollection<GetBookResponse>>(books);
        }
    }
}