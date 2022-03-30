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

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            await _movieRepository.CreateAsync(movie);
            return Ok(movie);
        }
    }
}
