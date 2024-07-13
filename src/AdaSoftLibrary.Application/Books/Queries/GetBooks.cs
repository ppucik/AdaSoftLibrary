using AdaSoftLibrary.Application.Books.Contracts;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Common;
using AutoMapper;
using MediatR;

namespace AdaSoftLibrary.Application.Books.Queries;

/// <summary>
/// Vrátí zoznam kníh
/// </summary>
public class GetBooks
{
    public class Query : IRequest<PagedList<GetBookResponse>>
    {
        /// <summary>
        /// Filter pre zoznam kníh
        /// </summary>
        public BookFilter BookFilter { get; set; } = new();

        /// <summary>
        /// Stránkovanie a radenie záznamov
        /// </summary>
        public Pagination Pagination { get; set; } = new();
    }

    public class QueryHandler : IRequestHandler<Query, PagedList<GetBookResponse>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public QueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedList<GetBookResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetListAsync(query.BookFilter, query.Pagination, cancellationToken);

            int pageNumber = query.Pagination.PageNumber > 0 ? query.Pagination.PageNumber : 1;
            int pageSize = query.Pagination.PageSize;
            int totalCount = books.Count(); // await _bookRepository.GetCouuntAsync(cancellationToken);

            //// Možné použiť len pre DB, ktorá implementuje 'IAsyncQueryProvider', ale ne pre XML
            //return await bookQuery
            //    .ProjectTo<GetBookResponse>(_mapper.ConfigurationProvider)
            //    .PaginatedListAsync(query.PageNumber, query.PageSize)

            return new PagedList<GetBookResponse>(_mapper.Map<IReadOnlyCollection<GetBookResponse>>(books), pageNumber, pageSize, totalCount);
        }
    }
}