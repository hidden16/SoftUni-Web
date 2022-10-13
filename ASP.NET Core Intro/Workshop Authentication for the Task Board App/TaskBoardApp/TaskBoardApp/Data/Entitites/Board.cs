using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants.Board;
namespace TaskBoardApp.Data.Entitites
{
    public class Board
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        [MinLength(MinNameLength)]
        public string Name { get; set; } = null!;

        public IEnumerable<Task> Tasks { get; set; }
            = new List<Task>();
    }
}