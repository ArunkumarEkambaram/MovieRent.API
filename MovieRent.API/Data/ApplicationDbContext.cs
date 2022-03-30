using Microsoft.EntityFrameworkCore;
using MovieRent.API.Data.Models;

namespace MovieRent.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> db) : base(db)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}
