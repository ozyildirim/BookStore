using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries;

public class GetGenresQuery
{
    private readonly IBookStoreDbContext _context;

    private readonly IMapper _mapper;

    public GetGenresQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _context = dbContext;
    }

    public List<GenreViewModel> Handle()
    {
        var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id).ToList();
        List<GenreViewModel> returnedGenres = _mapper.Map<List<GenreViewModel>>(genres);
        return returnedGenres;
    }
}

public class GenreViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
