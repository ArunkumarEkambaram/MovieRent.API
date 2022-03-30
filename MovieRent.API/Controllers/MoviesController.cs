using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRent.API.Data.Models;
using MovieRent.API.Interfaces;
using System.Threading.Tasks;

namespace MovieRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpPost("AddNewMovie")]
        public async Task<IActionResult> Create([FromBody]Movie movie)
        {
            await _movieRepository.CreateAsync(movie);
            return Ok(movie);
        }

        [HttpGet("GetAllMovie")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllAsync();
            return Ok(movies);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPut("UpdateMovie/{id}")]
        public async Task<IActionResult> UpdateMovie(int? id, [FromBody] Movie movie)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = await _movieRepository.UpdateAsync(id.Value, movie);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("DeleteMovie/{id}")]
        public async Task<IActionResult> DeleteMovie(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = await _movieRepository.DeleteAsync(id.Value);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
