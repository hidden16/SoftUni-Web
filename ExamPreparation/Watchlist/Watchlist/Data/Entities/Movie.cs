using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Watchlist.Data.Constants.GlobalConstants.Movie;
namespace Watchlist.Data.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DirectorMaxLength)]
        public string Director { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(4,2)")]
        public decimal Rating { get; set; }

        public int? GenreId { get; set; }
        [ForeignKey(nameof(GenreId))]
        public Genre? Genre { get; set; }
        public List<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
    }
}