using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Watchlist.Data.Entities;
using static Watchlist.Data.Constants.GlobalConstants.Movie;
namespace Watchlist.Models
{
    public class AddMovieViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DirectorMaxLength, MinimumLength = DirectorMinLength)]
        public string Director { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(4,2)")]
        public decimal Rating { get; set; }

        public int GenreId { get; set; }
        public IEnumerable<Genre> Genre { get; set; } = new List<Genre>();
    }
}
