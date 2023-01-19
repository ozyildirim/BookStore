using AutoMapper;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace WebApi.Application.BookOperations.Commands;

public class UpdateBookCommand
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int BookId { get; set; }
    public UpdateBookModel Model { get; set; }

    public UpdateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == BookId);

        if (book is null)
        {
            throw new InvalidOperationException("Book not found!");
        }

        // book = _mapper.Map<Book>(Model);
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.Title = Model.Title != default ? Model.Title : book.Title;

        _context.SaveChanges();
    }
}

public class UpdateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
}
