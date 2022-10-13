using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants.User;

namespace TaskBoardApp.Data.Entitites
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(MaxUserFirstNameLength)]
        public string FirstName { get; init; } = null!;
        [Required]
        [MaxLength(MaxUserLastNameLength)]
        public string LastName { get; init; } = null!;
    }
}
