using CS321_W4D1_BookAPI.Models;
using CS321_W4D1_BookAPI.ApiModels;
using CS321_W4D1_BookAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CS321_W4D1_BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        // TODO: get all publisher
        // GET api/pubisher
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_publisherService.GetAll());
        }

        // GET: api/Publishers
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var publisher = _publisherService.Get(id);
            if (publisher== null) return NotFound();
            return Ok(publisher);
        }

        // POST: api/Publishers
        [HttpPost]
        public IActionResult Post([FromBody] Publisher newPublisher)
        {
            try
            {
                // add the new publisher
                _publisherService.Add(newPublisher);
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("AddAuthor", ex.GetBaseException().Message);
                return BadRequest(ModelState);
            }

            // return a 201 Created status. This will also add a "location" header
            // with the URI of the new author. E.g., /api/authors/99, if the new is 99
            return CreatedAtAction("Get", new { Id = newPublisher.Id }, newPublisher);
        }
        // PUT: api/Publishers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Publisher updatedPublisher)
        {
            var publisher = _publisherService.Update(updatedPublisher);
            if (publisher == null) return NotFound();
            return Ok(publisher);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var publisher = _publisherService.Get(id);
            if (publisher == null) return NotFound();
            _publisherService.Remove(publisher);
            return NoContent();
        }
    }

}
