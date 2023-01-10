using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : Controller
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1, //Personal Growth
                PageCount = 100,
                PublishedDate = new DateTime(2000, 1, 1)
            },
            new Book
            {
                Id = 2,
                Title = "Herland",
                GenreId = 2, //Science Fiction
                PageCount = 250,
                PublishedDate = new DateTime(2010, 05, 23)
            },
            new Book
            {
                Id = 3,
                Title = "Dune",
                GenreId = 2, //Science Fiction
                PageCount = 540,
                PublishedDate = new DateTime(2001, 12, 2)
            },
        };

        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        // api/Books/1
        public Book GetByID(int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)
            {
                return BadRequest();
            }

            BookList.Add(newBook);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);

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
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            BookList.Remove(book);
            return Ok();
        }
    }
}
