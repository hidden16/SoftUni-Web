using Microsoft.AspNetCore.Mvc;
using Watchlist.Data.Entities;
using Watchlist.Models;

namespace Watchlist.Data.Common
{
    public interface IMovieService
    {
        public Task<IEnumerable<MovieViewModel>> AllMoviesAsync();
        public Task<IEnumerable<Genre>> GetAllGenreAsync();
        public Task AddMovie(AddMovieViewModel viewModel);
        public Task AddToCollection(int movieId, string userId);
        public Task<IEnumerable<MovieViewModel>> WatchedMovies(string userId);
        public Task RemoveFromCollection(int movieId, string userId);
    }
}
