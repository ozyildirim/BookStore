using AutoMapper;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace WebApi.Application.GenreOperations.Commands;

public class CreateGenreCommand
{
    public CreateGenreModel Model { get; set; }
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);

        if (genre is not null)
        {
            throw new InvalidOperationException("Genre already exists!");
        }

        // Map create view model to entity
        genre = _mapper.Map<Genre>(Model);

        _context.Genres.Add(genre);
        _context.SaveChanges();
    }
}

public class CreateGenreModel
{
    public string Name { get; set; }
    public bool isActive { get; set; }
}
