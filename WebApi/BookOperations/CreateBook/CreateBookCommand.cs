using WebApi.DBOperations;
using WebApi.Models;

namespace WebApi.BookOperations.CreateBook;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }

    private readonly BookStoreDbContext _dbContext;

    public CreateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        if (book is not null)
        {
            throw new InvalidOperationException("Kitap zaten mevcut!");
        }
        book = new Book
        {
            Title = Model.Title,
            GenreId = Model.GenreId,
            PublishedDate = Model.PublishedDate,
            PageCount = Model.PageCount,
        };

        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }

    public class CreateBookModel
    {
        public string? Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
