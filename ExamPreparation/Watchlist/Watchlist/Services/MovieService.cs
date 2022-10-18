using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Watchlist.Data;
using Watchlist.Data.Common;
using Watchlist.Data.Entities;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;
        public MovieService(WatchlistDbContext context)
        {
            this.context = context;
        }

        public async Task AddMovie(AddMovieViewModel viewModel)
        {
            await context.Movies.AddAsync(new Movie()
            {
                GenreId = viewModel.GenreId,
                ImageUrl = viewModel.ImageUrl,
                Director = viewModel.Director,
                Rating = viewModel.Rating,
                Title = viewModel.Title,
                Id = viewModel.Id,
            });

            await context.SaveChangesAsync();
        }

        public async Task AddToCollection(int movieId, string userId)
        {
            var user = await context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.UsersMovies)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid User Id");
            }

            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);

            if (movie == null)
            {
                throw new ArgumentException("Invalid Movie Id");
            }

            if (!user.UsersMovies.Any(u => u.MovieId == movieId))
            {
                user.UsersMovies.Add(new UserMovie()
                {
                    MovieId = movieId,
                    UserId = userId,
                });
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MovieViewModel>> AllMoviesAsync()
        {
            var entities = await context.Movies
                .Include(x => x.Genre)
                 .ToListAsync();

            return entities
                .Select(e => new MovieViewModel()
                {
                    Genre = e.Genre.Name,
                    Director = e.Director,
                    Id = e.Id,
                    ImageUrl = e.ImageUrl,
                    Rating = e.Rating,
                    Title = e.Title
                });
        }

        public async Task<IEnumerable<Genre>> GetAllGenreAsync()
        {
            return await context.Genres.ToListAsync();
        }

        public async Task RemoveFromCollection(int movieId, string userId)
        {
            var user = await context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.UsersMovies)
                .ThenInclude(x => x.Movie.Genre)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid User");
            }

            var movie = await context.Movies
                .Where(x => x.Id == movieId)
                .ToListAsync();

            if (movie == null)
            {
                throw new ArgumentException("Invalid Movie");
            }
            var userMovie = user.UsersMovies.Where(x => x.MovieId == movieId).FirstOrDefault();
            user.UsersMovies.Remove(userMovie);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> WatchedMovies(string userId)
        {
            var user = await context.Users
                 .Where(x => x.Id == userId)
                 .Include(x => x.UsersMovies)
                 .ThenInclude(x=>x.Movie)
                 .ThenInclude(x=>x.Genre)
                 .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user");
            }

            var result = user.UsersMovies.Where(x=>x.UserId == userId).ToList();
            List<MovieViewModel> movies = new List<MovieViewModel>();
            foreach (var item in result)
            {
                movies.Add(new MovieViewModel()
                {
                    Director = item.Movie.Director,
                    Title = item.Movie.Title,
                    Rating = item.Movie.Rating,
                    Id = item.Movie.Id,
                    ImageUrl = item.Movie.ImageUrl,
                    Genre = item.Movie.Genre.Name
                });
            }
            return movies;
        }
    }
}
