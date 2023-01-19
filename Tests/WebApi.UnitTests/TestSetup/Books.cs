using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace TestSetup;

public static class Books
{
    public static void AddBooks(this BookStoreDbContext context)
    {
        context.Books.AddRange(
            new Book
            {
                Title = "Lean Startup",
                GenreId = 1, //Personal Growth
                PageCount = 100,
                PublishedDate = new DateTime(2000, 1, 1),
                AuthorId = 1
            },
            new Book
            {
                Title = "Herland",
                GenreId = 2, //Science Fiction
                PageCount = 250,
                PublishedDate = new DateTime(2010, 05, 23),
                AuthorId = 2
            },
            new Book
            {
                Title = "Dune",
                GenreId = 2, //Science Fiction
                PageCount = 540,
                PublishedDate = new DateTime(2001, 12, 2),
                AuthorId = 2
            }
        );
    }
}
