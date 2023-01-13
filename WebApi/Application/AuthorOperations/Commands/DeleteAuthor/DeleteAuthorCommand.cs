using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands;

public class DeleteAuthorCommand
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int AuthorId { get; set; }

    public DeleteAuthorCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

        if (author is null)
            throw new InvalidOperationException("Author not found!");

        var authorBooks = _context.Books.Where(x => x.AuthorId == AuthorId).ToList();

        if (authorBooks.Count > 0)
        {
            throw new InvalidOperationException(
                "Author has more than one released book! Delete them first!"
            );
        }

        _context.Remove(author);
        _context.SaveChanges();
    }
}
