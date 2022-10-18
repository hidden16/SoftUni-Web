using System.ComponentModel.DataAnnotations;
using static Watchlist.Data.Constants.GlobalConstants.Genre;
namespace Watchlist.Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}