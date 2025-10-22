using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pubs.Api.DTOs;
using Pubs.API.Interfaces;

namespace Pubs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController(IPublisherService publisherService) : ControllerBase
    {
        private readonly IPublisherService _publisherService = publisherService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> GetPublishers()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDto>> GetPublisher(string id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
            if (publisher == null)
                return NotFound();
            return Ok(publisher);
        }

        [HttpGet("{id}/titles")]
        public async Task<ActionResult<IEnumerable<TitleDto>>> GetPublisherTitles(string id)
        {
            var titles = await _publisherService.GetPublisherTitlesAsync(id);
            return Ok(titles);
        }

        [HttpPost]
        public async Task<ActionResult<PublisherDto>> CreatePublisher(CreatePublisherDto publisherDto)
        {
            try
            {
                var createdPublisher = await _publisherService.CreatePublisherAsync(publisherDto);
                return CreatedAtAction(nameof(GetPublisher), new { id = createdPublisher.PubId }, createdPublisher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(string id, UpdatePublisherDto publisherDto)
        {
            try
            {
                await _publisherService.UpdatePublisherAsync(id, publisherDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(string id)
        {
            try
            {
                await _publisherService.DeletePublisherAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}