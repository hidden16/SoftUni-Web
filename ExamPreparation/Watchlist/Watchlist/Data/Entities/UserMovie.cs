namespace Watchlist.Data.Entities
{
    public class UserMovie
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
