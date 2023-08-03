using Microsoft.AspNetCore.Identity;

namespace MembershipApplication.Data
{
    public class ApplicationUser: IdentityUser<Int64>
    {
       public string Name { get; set; }
       public string ProfilePicture { get; set; }
    }
}
