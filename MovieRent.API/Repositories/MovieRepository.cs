using Microsoft.EntityFrameworkCore;
using MovieRent.API.Data;
using MovieRent.API.Data.Models;
using MovieRent.API.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRent.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Movie entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Movies.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movieInDb = await _context.Movies.FindAsync(id);
            if (movieInDb == null)
            {
                return false;
            }
            _context.Movies.Remove(movieInDb);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<Movie>> GetAllAsync()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<IList<Movie>> GetMovieByRating(int rating)
        {
            var movies = await _context.Movies.Where(x => x.Rating >= rating).ToListAsync();
            return movies;
        }

        public async Task<bool> UpdateAsync(int id, Movie entity)
        {           
            var movieInDb = await _context.Movies.FindAsync(id);
            if (movieInDb == null)
            {
                return false;
            }

            movieInDb.MovieName = entity.MovieName;
            movieInDb.Rating = entity.Rating;
            movieInDb.Genre = entity.Genre;
            movieInDb.CoverUrl = entity.CoverUrl;
            movieInDb.ModifiedDate = entity.ModifiedDate;
            _context.Entry(movieInDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
