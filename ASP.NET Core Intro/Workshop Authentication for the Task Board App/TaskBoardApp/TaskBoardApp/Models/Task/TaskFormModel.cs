using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants.Task;

namespace TaskBoardApp.Models.Task
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(MaxTitleLength, MinimumLength = MinTitleLength,
            ErrorMessage = "Title should be atleast {2} characters long.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength,
            ErrorMessage = "Description should be atleast {2} characters long.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Board")]
        public int BoardId { get; set; }
        public IEnumerable<TaskBoardModel> Boards { get; set; }
    }
}
