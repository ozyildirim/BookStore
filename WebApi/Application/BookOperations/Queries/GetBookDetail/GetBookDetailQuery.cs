using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries;

public class GetBookDetailQuery
{
    private readonly BookStoreDbContext _dbContext;
    public int BookId { get; set; }
    private readonly IMapper _mapper;

    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        if (book is null)
            throw new InvalidOperationException("Book not found!");

        BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
        return vm;
    }
}

public class BookDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishedDate { get; set; }
}
