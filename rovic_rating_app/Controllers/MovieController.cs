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
    public class MovieController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new GetMovieByIdRequest(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetUserMovies(int userId)
        {
            var result = await _mediator.Send(new GetMovieByUserIdRequest(userId));

            return Ok(result);
        }

        [HttpGet("search/{text}")]
        public async Task<IActionResult> SearchMovie(string text)
        {
            var result = await _mediator.Send(new SearchMovieQueryRequest(text));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostMovie([FromBody] MoviePostDTO movie)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateMovieCommandRequest(movie));
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{movieId:int}/tag/{tagId:int}")]
        public async Task<IActionResult> AddTag(int movieId, int tagId)
        {
            if (movieId == 0 || tagId == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new AddTagToMovieRequest(movieId, tagId));

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieUpdateDTO movie)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdateMovieCommandRequest(movie));
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new DeleteMovieRequest(id));

            if (result == false)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
