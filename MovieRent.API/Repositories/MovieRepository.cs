using MovieRent.API.Data;
using MovieRent.API.Data.Models;
using MovieRent.API.Interfaces;
using System.Collections.Generic;
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

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Movie>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Movie>> GetMovieByRating(int rating)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, Movie entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
