using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries;

public class GetGenreDetailQuery
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int? GenreId { get; set; }

    public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public GenreDetailViewModel Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);

        if (genre is null)
            throw new InvalidOperationException("Genre not found!");

        GenreDetailViewModel returnedObject = _mapper.Map<GenreDetailViewModel>(genre);
        return returnedObject;
    }
}

public class GenreDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
