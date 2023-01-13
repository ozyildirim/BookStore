using AutoMapper;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace WebApi.Application.GenreOperations.Commands;

public class UpdateGenreCommand
{
    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateGenreCommand(BookStoreDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    // Update DB
    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);

        if (genre is null)
            throw new InvalidOperationException("Genre not found!");

        // genre = _mapper.Map<Genre>(Model);
        genre.IsActive = Model.IsActive != default ? Model.IsActive : genre.IsActive;
        genre.Name = Model.Name != default ? Model.Name : genre.Name;
        _context.SaveChanges();
    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}
