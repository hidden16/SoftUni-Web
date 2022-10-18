using System.ComponentModel.DataAnnotations;
using static Watchlist.Data.Constants.GlobalConstants.User;
namespace Watchlist.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;
        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
