using AutoMapper;
using WebApi.DBOperations;

namespace TestSetup;

public class CommonTestFixture
{
    public BookStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }
}
