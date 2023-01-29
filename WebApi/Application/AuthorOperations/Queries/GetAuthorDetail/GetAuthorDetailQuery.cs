using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries;

public class GetAuthorDetailQuery
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int? AuthorId { get; set; }

    public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public AuthorDetailViewModel Handle()
    {
        var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

        if (author is null)
            throw new InvalidOperationException("Author not found!");

        AuthorDetailViewModel model = _mapper.Map<AuthorDetailViewModel>(author);
        return model;
    }
}

public class AuthorDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }
}
