using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetailQuery;

public class GetBookDetalQuery
{
    private readonly BookStoreDbContext _dbContext;
    public int BookId { get; set; }
    private readonly IMapper _mapper;

    public GetBookDetalQuery(IMapper mapper, BookStoreDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        if (book is null)
            throw new InvalidOperationException("Kitap Bulunamadı!");

        BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
        return vm;
    }
}

public class BookDetailViewModel
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
}
