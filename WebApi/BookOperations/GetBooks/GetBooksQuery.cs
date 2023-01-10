using WebApi.DBOperations;
using WebApi.Models;

namespace WebApi.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext _dbContext;

    public GetBooksQuery(BookStoreDbContext dbOperations)
    {
        _dbContext = dbOperations;
    }

    public List<BookViewModel> Handle()
    {
        var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList();
        List<BookViewModel> vm = new List<BookViewModel>();

        foreach (var book in bookList)
        {
            vm.Add(
                new BookViewModel
                {
                    Title = book.Title,
                    PublishDate = book.PublishedDate.Date.ToString("dd/MM/yyyy"),
                    PageCount = book.PageCount,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                }
            );
        }
        return vm;
    }

    public class BookViewModel
    {
        public string? Title { get; set; }
        public int PageCount { get; set; }
        public string? PublishDate { get; set; }
        public string? Genre { get; set; }
    }
}
