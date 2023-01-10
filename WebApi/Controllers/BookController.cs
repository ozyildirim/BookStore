using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : Controller
    {
        private readonly BookStoreDbContext _context;

        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger, BookStoreDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        // api/Books/1
        public Book GetByID(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)
            {
                return BadRequest();
            }

            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if (book is null)
            {
                return BadRequest();
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount =
                updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            book.PublishedDate =
                updatedBook.PublishedDate != default
                    ? updatedBook.PublishedDate
                    : book.PublishedDate;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
