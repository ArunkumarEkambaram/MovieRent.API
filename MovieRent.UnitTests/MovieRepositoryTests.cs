using Microsoft.EntityFrameworkCore;
using MovieRent.API.CustomExceptions;
using MovieRent.API.Data;
using MovieRent.API.Data.Models;
using MovieRent.API.Repositories;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRent.UnitTests
{
    public class MovieRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> dbContextOptions =
            new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MovieRepoDB").Options;

        private ApplicationDbContext dbContext;

        private MovieRepository movie;

        [OneTimeSetUp]
        public void Setup()
        {
            dbContext = new ApplicationDbContext(dbContextOptions);
            movie = new MovieRepository(dbContext);
            dbContext.Database.EnsureCreated();
            PopulateMovies();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            dbContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetById_WhenEnteredIncorrectMovieId_ReturnNull()
        {
            //Act
            var result = await movie.GetByIdAsync(10);

            //Assert
            //Assert.IsNull(result);
            Assert.That(result, Is.Null);//Fluent API
        }

        [Test]
        public async Task GetById_WhenEnteredCorrectMovieId_ReturnAMovie()
        {
            //Act
            var result = await movie.GetByIdAsync(1);

            //Assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.MovieName, Is.EqualTo("Movie 1"));
        }

        [Test]
        public void CreateMovie_WhenMovieIdIsZero_ThrowsException()
        {
            Movie m = new Movie { Id = 0 };
            Assert.That(async () => await movie.CreateAsync(m),
                Throws.Exception.TypeOf<MovieException>().With.Message.EqualTo("Movie id cannot be zero"));
        }

        [Test]
        public async Task CreateMovie_WhenCorrectValuePasses_ReturnsTrue()
        {
            Movie m = new Movie { Id = 4, MovieName = "Movie 4" };
            var result = await movie.CreateAsync(m);
            Assert.That(result, Is.True);
        }

        public void PopulateMovies()
        {
            var movies = new List<Movie>
            {
                new Movie{Id=1, MovieName="Movie 1", Genre="Action"},
                new Movie{Id=2, MovieName="Movie 2", Genre="Thriller"},
                new Movie{Id=3, MovieName="Movie 3", Genre="Family"},
            };

            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();
        }
    }
}
