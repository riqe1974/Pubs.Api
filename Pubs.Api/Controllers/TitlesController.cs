using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pubs.Api.DTOs;
using Pubs.API.Interfaces;


namespace Pubs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TitlesController(ITitleService titleService) : ControllerBase
    {
        private readonly ITitleService _titleService = titleService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TitleDto>>> GetTitles()
        {
            var titles = await _titleService.GetAllTitlesAsync();
            return Ok(titles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TitleDto>> GetTitle(string id)
        {
            var title = await _titleService.GetTitleByIdAsync(id);
            return title == null ? (ActionResult<TitleDto>)NotFound() : (ActionResult<TitleDto>)Ok(title);
        }

        [HttpPost]
        public async Task<ActionResult<TitleDto>> CreateTitle(CreateTitleDto titleDto)
        {
            try
            {
                var createdTitle = await _titleService.CreateTitleAsync(titleDto);
                return CreatedAtAction(nameof(GetTitle), new { id = createdTitle.TitleId }, createdTitle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTitle(string id, CreateTitleDto titleDto)
        {
            try
            {
                await _titleService.UpdateTitleAsync(id, titleDto);
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
        public async Task<IActionResult> DeleteTitle(string id)
        {
            try
            {
                await _titleService.DeleteTitleAsync(id);
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
