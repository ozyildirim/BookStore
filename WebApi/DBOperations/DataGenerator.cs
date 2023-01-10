using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (
            var context = new BookStoreDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()
            )
        )
        {
            if (context.Books.Any())
            {
                return;
            }
            context.Books.AddRange(
                new Book
                {
                    Id = 1,
                    Title = "Lean Startup",
                    GenreId = 1, //Personal Growth
                    PageCount = 100,
                    PublishedDate = new DateTime(2000, 1, 1)
                },
                new Book
                {
                    Id = 2,
                    Title = "Herland",
                    GenreId = 2, //Science Fiction
                    PageCount = 250,
                    PublishedDate = new DateTime(2010, 05, 23)
                },
                new Book
                {
                    Id = 3,
                    Title = "Dune",
                    GenreId = 2, //Science Fiction
                    PageCount = 540,
                    PublishedDate = new DateTime(2001, 12, 2)
                }
            );

            context.SaveChanges();
        }
    }
}
