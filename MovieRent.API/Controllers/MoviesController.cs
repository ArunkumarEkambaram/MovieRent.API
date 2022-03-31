using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRent.API.CustomExceptions;
using MovieRent.API.Data.Models;
using MovieRent.API.Interfaces;
using System;
using System.Threading.Tasks;

namespace MovieRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieRepository movieRepository, ILogger<MoviesController> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        [HttpPost("AddNewMovie")]
        public async Task<IActionResult> Create([FromBody] Movie movie)
        {
            try
            {
                if (movie.Id == 0)
                {
                    throw new MovieException("Movie Id cannot be zero");
                }
                await _movieRepository.CreateAsync(movie);
                return Ok(movie);
            }
            catch (Exception ex) // when (ex.GetType()==typeof(MovieException))
            {             
                if (ex.GetType() == typeof(MovieException))
                {
                    _logger.LogError(ex.Message);                   
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                //throw new MovieException("Unable to fetching movie details");
                _logger.LogInformation("Fetching Movie Details");
                var movies = await _movieRepository.GetAllAsync();
                _logger.LogInformation($"Total items fetched :{movies.Count}");
                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred :{ex}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var movie = await _movieRepository.GetByIdAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred :{ex}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateMovie/{id}")]
        public async Task<IActionResult> UpdateMovie(int? id, [FromBody] Movie movie)
        {

            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _movieRepository.UpdateAsync(id.Value, movie);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred :{ex}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteMovie/{id}")]
        public async Task<IActionResult> DeleteMovie(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _movieRepository.DeleteAsync(id.Value);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred :{ex}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
