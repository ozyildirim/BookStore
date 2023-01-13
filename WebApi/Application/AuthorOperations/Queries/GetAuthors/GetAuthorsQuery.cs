using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries;

public class GetAuthorsQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<AuthorViewModel> Handle()
    {
        var result = _context.Authors.OrderBy(x => x.Id).ToList();
        List<AuthorViewModel> list = _mapper.Map<List<AuthorViewModel>>(result);
        return list;
    }
}

public class AuthorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }
}
