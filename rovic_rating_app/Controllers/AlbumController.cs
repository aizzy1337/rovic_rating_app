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
    public class AlbumController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AlbumController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new GetAlbumByIdRequest(id));

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetUserAlbums(int userId)
        {
            var result = await _mediator.Send(new GetAlbumByUserIdRequest(userId));

            return Ok(result);
        }

        [HttpGet("search/{text}")]
        public async Task<IActionResult> SearchAlbum(string text)
        {
            var result = await _mediator.Send(new SearchAlbumQueryRequest(text));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAlbum([FromBody] AlbumPostDTO album)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateAlbumCommandRequest(album));
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{albumId:int}/tag/{tagId:int}")]
        public async Task<IActionResult> AddTag(int albumId, int tagId)
        {
            if(albumId == 0 || tagId == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new AddTagToAlbumRequest(albumId, tagId));

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAlbum([FromBody] AlbumUpdateDTO album)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdateAlbumCommandRequest(album));
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new DeleteAlbumRequest(id));

            if(result == false)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
