using Microsoft.AspNetCore.Identity;

namespace Library.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<ApplicationUserBook> ApplicationUsersBooks { get; set; } = new List<ApplicationUserBook>();
    }
}
