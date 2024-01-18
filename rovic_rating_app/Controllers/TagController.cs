using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rovic_rating_app.Handlers;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;

namespace rovic_rating_app.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Administrator")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("movie/user/{userId:int}")]
        public async Task<IActionResult> GetMovieTags(int userId)
        {
            if (userId == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new GetMovieTagsRequest(userId));

            return Ok(result);
        }

        [HttpGet("movie/{movieId:int}")]
        public async Task<IActionResult> GetTagsByMovie(int movieId)
        {
            if (movieId == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new GetTagsByMovieRequest(movieId));

            return Ok(result);
        }

        [HttpGet("album/user/{userId:int}")]
        public async Task<IActionResult> GetAlbumTags(int userId)
        {
            if (userId == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new GetAlbumTagsRequest(userId));

            return Ok(result);
        }

        [HttpGet("album/{albumId:int}")]
        public async Task<IActionResult> GetTagsByAlbum(int albumId)
        {
            if (albumId == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new GetTagsByAlbumRequest(albumId));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostTag([FromBody] TagPostDTO tag)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateTagCommandRequest(tag));
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTag(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new DeleteTagRequest(id));

            if (result == false)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
