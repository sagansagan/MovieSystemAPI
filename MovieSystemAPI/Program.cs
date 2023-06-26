using Microsoft.EntityFrameworkCore;
using MovieSystemAPI.Data;
using MovieSystemAPI.Models;
using System.Globalization;
using System.Xml.Linq;

namespace MovieSystemAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MovieDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddCors(options => options.AddPolicy("MyPolicy", builder => 
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }
                )
            );
            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            

            //hämtar alla personer
            app.MapGet("/person", async (MovieDbContext context) =>
                await context.Persons.ToListAsync());

            //hämtar alla genrer
            app.MapGet("/genre", async (MovieDbContext context) => 
                await context.Genres.ToListAsync());

            //hämtar alla genrer som är kopplade till en specifik person
            app.MapGet("/person/genre", async (string firstName, MovieDbContext context) =>
            {
                var personGenres = from x in context.PersonGenres
                                   select new
                                   {
                                       x.Persons.FirstName,
                                       x.Genres.Name
                                   };

                return await personGenres.Where(x => x.FirstName == firstName).ToListAsync();
            })
            .WithName("PersonIdGenre");

            //Hämtar alla filmer som är kopplade till en specifik person
            app.MapGet("/person/movie", async (string firstName, MovieDbContext context) =>
            {
                var personMovies = from x in context.PersonGenres
                                   select new
                                   {
                                       x.Persons.FirstName,
                                       x.Movie
                                   };

                return await personMovies.Where(x => x.FirstName == firstName).ToListAsync();
            })
            .WithName("PersonIdMovie");

            //hämtar "rating" på filmer kopplat till en person
            app.MapGet("/person/ratings", async (string firstName, MovieDbContext context) =>
            {
                var movieRating = from x in context.PersonGenres
                                  select new
                                  {
                                      x.Persons.FirstName,
                                      x.Movie,
                                      x.Rating
                                  };

                return await movieRating.Where(x => x.FirstName == firstName).ToListAsync();
            })
            .WithName("PersonIdRatings");

            //Lägger in ratings på filmer kopplat till en person
            app.MapPost("/person/add-rating", async (MovieDbContext context, int rating, string movieName, int personId, int genreId) =>
            {

                var movie = await context.PersonGenres.FirstOrDefaultAsync(mv => mv.Movie == movieName);


                if (movie == null)
                {
                    return Results.NotFound();
                }
                var movieRating = context.PersonGenres;
                movieRating.Add(new PersonGenre { Rating = rating, Movie = movieName, PersonId = personId, GenreId=genreId });

                await context.SaveChangesAsync();

                return Results.Created($"/person/ratings/", rating);


            })
            .WithName("PersonAddRating");

            //Koppla en person till en ny genre

            app.MapPost("/person/add-genre", async (MovieDbContext context, int personId, int genreId) =>
            {
                var newGenre = new PersonGenre
                {
                    PersonId = personId,
                    GenreId = genreId
                };
                await context.PersonGenres.AddAsync(newGenre);
                await context.SaveChangesAsync();
            })
                .WithName("PersonAddGenre");

            //Lägga in nya länkar/filmer för en specifik person och en specifik genre
            app.MapPost("/person/add-movie", async (MovieDbContext context, int personId, string movieName, int genreId) =>
            {
                var newMovie = new PersonGenre
                {
                    PersonId = personId,
                    Movie = movieName,
                    GenreId= genreId

                };
                await context.PersonGenres.AddAsync(newMovie);
                await context.SaveChangesAsync();
            })
                .WithName("PersonAddMovie");

            //Få förslag på filmer i en viss genre från ett externt API, t.ex TMDB


            app.MapGet("/recommendations", async (MovieDbContext context, string genreName) =>
            {
                var genre = await context.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
                //insert apikey here
                var apiKey = " ";
                var url = $"https://api.themoviedb.org/3/discover/movie?api_key={apiKey}&sort_by=popularity.desc&include_adult=false&include_video=false&with_genres={genre.GenreId}&with_watch_monetization_types=free";

                var client = new HttpClient();

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return Results.Content(content, contentType: "application/json");
            });


            app.Run();
        }
    }
}