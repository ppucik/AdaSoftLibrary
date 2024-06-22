using AdaSoftLibrary.Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Queries;

/// <summary>
/// Vrátí informácie o kníhe
/// </summary>
public class GetBook
{
    public class Query : IRequest<GetBookResponse>
    {
        public Query(int id) { Id = id; }
        public int Id { get; set; }
    }

    public class QueryHandler : IRequestHandler<Query, GetBookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public QueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetBookResponse> Handle(Query query, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(query.Id, cancellationToken);

            return _mapper.Map<GetBookResponse>(book);
        }
    }
}