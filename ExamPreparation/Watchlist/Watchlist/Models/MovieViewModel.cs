using System.ComponentModel.DataAnnotations;
using Watchlist.Data.Entities;
using static Watchlist.Data.Constants.GlobalConstants.Movie;
namespace Watchlist.Models
{
    public class MovieViewModel
    {
        [Key]
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
        [Range(typeof(decimal),"0.00","10.00")]
        public decimal Rating { get; set; }

        public string Genre { get; set; }
        
    }
}
