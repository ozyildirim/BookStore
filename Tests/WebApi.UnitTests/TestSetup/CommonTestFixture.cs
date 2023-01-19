using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup;

public class CommonTestFixture
{
    public BookStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture(BookStoreDbContext context, IMapper mapper)
    {
        var options = new DbContextOptionsBuilder<BookStoreDbContext>()
            .UseInMemoryDatabase(databaseName: "BookStoreTestDB")
            .Options;

        Context = new BookStoreDbContext(options);
        Context.Database.EnsureCreated();
        Context.AddBooks();
        Context.AddGenres();
        Context.SaveChanges();

        Mapper = new MapperConfiguration(
            config => config.AddProfile<MappingProfile>()
        ).CreateMapper();
    }
}
