using Microsoft.AspNetCore.Mvc;
using my_books.ActionResults;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        public PublishersServices _publisherService;

        public PublisherController(PublishersServices publisherService)
        {
            _publisherService = publisherService;
        }
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publisherService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string? orderBy, string? searchString, int? pageNumber) 
        {
            try
            {
                var _result = _publisherService.GetAllPublishers(orderBy, searchString, pageNumber).ToList();
                return Ok(_result);
            }
            catch (Exception)
            {
                return BadRequest("No publishers could be loaded.");
            }
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publisherService.GetPublisherById(id);
            if (_response != null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publisherService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publisherService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-all-publishers")]
        public IActionResult DeleteAllPublishers()
        {
            _publisherService.DeleteAllPublishers();
            return Ok();
        }
    }
}
