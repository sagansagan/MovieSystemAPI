using Microsoft.EntityFrameworkCore;
using MovieSystemAPI.Models;

namespace MovieSystemAPI.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PersonGenre> PersonGenres { get; set; }

        
    }
}
