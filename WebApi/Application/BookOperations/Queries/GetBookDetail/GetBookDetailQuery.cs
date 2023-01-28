using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries;

public class GetBookDetailQuery
{
    private readonly IBookStoreDbContext _dbContext;
    public int? BookId { get; set; }
    private readonly IMapper _mapper;

    public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books
            .Include(x => x.Genre)
            .Include(x => x.Author)
            .SingleOrDefault(x => x.Id == BookId);

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
    public string Author { get; set; }
    public int PageCount { get; set; }
    public string PublishedDate { get; set; }
}
