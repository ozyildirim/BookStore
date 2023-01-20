using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

namespace WebApi.DBOperations;

public class BookStoreDbContext : DbContext, IBookStoreDbContext
{
    // protected readonly IConfiguration Configuration;

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

    // public BookStoreDbContext(IConfiguration configuration) => Configuration = configuration;

    // protected override void OnConfiguring(DbContextOptionsBuilder options) =>
    //     options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));

    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Author> Authors { get; set; }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
}
