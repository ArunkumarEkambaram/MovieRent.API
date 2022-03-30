using MovieRent.API.Data.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRent.API.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IList<Movie>> GetMovieByRating(int rating);
    }
}
