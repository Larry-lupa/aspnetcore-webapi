using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    public class AuthorsController : ControllerBase
    {
        public AuthorsService _authorService;

        public AuthorsController(AuthorsService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("add-author")]
        public IActionResult AddBook([FromBody] AuthorVM author)
        {
            _authorService.AddAuthor(author);
            return Ok();
        }

        [HttpGet("get-author-with-books-by-id/{id}")]
        public IActionResult GetAuthorsWithBookById(int id)
        {
            var _response = _authorService.GetAuthorsWithBooksById(id);
            return Ok(_response);
        }
    }
}
