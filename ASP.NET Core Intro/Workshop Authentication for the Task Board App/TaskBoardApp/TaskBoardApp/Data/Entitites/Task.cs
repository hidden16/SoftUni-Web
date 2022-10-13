using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants.Task;
namespace TaskBoardApp.Data.Entitites
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]
        [MinLength(MinTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(MaxDescriptionLength)]
        [MinLength(MinDescriptionLength)]
        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int?BoardId { get; set; }
        public Board? Board { get; init; }

        [Required]
        public string OwnerId { get; set; } = null!;
        public User User { get; init; } = null!;
    }
}
