using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

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

            context.Genres.AddRange(
                new Genre { Name = "Personal Growth", },
                new Genre { Name = "Science Fiction" },
                new Genre { Name = "Novel" }
            );

            context.Authors.AddRange(
                new Author
                {
                    Name = "Kutay",
                    Surname = "Yıldırım",
                    Birthdate = new DateTime(1998, 11, 25).Date
                },
                new Author
                {
                    Name = "Çağan",
                    Surname = "Bıçakçı",
                    Birthdate = new DateTime(1998, 09, 12).Date
                },
                new Author
                {
                    Name = "Hasan",
                    Surname = "Taşkın",
                    Birthdate = new DateTime(1998, 1, 13).Date
                }
            );

            context.SaveChanges();
        }
    }
}
