using MembershipApplication.Data;
using MembershipApplication.DTO;
using Microsoft.AspNetCore.Identity;

namespace MembershipApplication.Repository
{
    public class UserAuthentication : IUserAuthentication
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserAuthentication(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Status> LoginAsync(LoginModel loginModel)
        {
            var status = new Status();
            //check login 
            var res1 = await signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, true, false);
            if (res1.Succeeded)
            {
                status.StatusCode = 1;
                status.Message = "User has logined successfully.";
                return status;
            }
            status.StatusCode = 0;
            status.Message = "Invalid Username and Password..";
            return status;

            //
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<Status> RegistrationAsync(Registration registration)
        {
            int i = 1;
            var status = new Status();
            var userExists = await userManager.FindByEmailAsync(registration.UserName);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists.";
                return status;
            }
            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = registration.Name,
                Email = registration.UserName,
                UserName = registration.UserName,
                EmailConfirmed = true,
                ProfilePicture = registration.Name
            };          
            var res = await userManager.CreateAsync(user, registration.Password);            
            if(!res.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed.";
                return status;
            }
            ApplicationRole applicationRole = new ApplicationRole()
            {
                Name = "SUPER_ADMIN",
                NormalizedName = "SUPER_ADMIN"
            };
            //Role management
            if (!await roleManager.RoleExistsAsync("SUPER_ADMIN"))
                await roleManager.CreateAsync(applicationRole);

            if (await roleManager.RoleExistsAsync("SUPER_ADMIN"))
            {
                await userManager.AddToRoleAsync(user, "SUPER_ADMIN");
            }

            //Get UserId
            Int64 userId = Convert.ToInt64(await userManager.GetUserIdAsync(user));

            //check login 
            if (i == 1)
            {
                var res1 = await signInManager.PasswordSignInAsync(registration.UserName, registration.Password, true, false);
                if (res1.Succeeded)
                {
                    status.StatusCode = 1;
                    status.Message = "User has logined successfully.";
                    return status;
                }
                status.StatusCode = 0;
                status.Message = "Invalid Username and Password..";
                return status;
            }

            // Reset Password

            if (i == 2)
            {
                //var users = ExecuteSqlQuery.GetAllMyAspNetUsersById($"select * from my_aspnet_users where id= {userObj.UserID}");
                
                var resreset = await userManager.ChangePasswordAsync(user,registration.Password,"newPassword");
                if (resreset.Succeeded)
                {
                    status.StatusCode = 1;
                    status.Message = "Password has reset successfully.";
                    return status;
                }
                status.StatusCode = 0;
                status.Message = "Password hasn't reset successfully.";
                return status;
            }

            status.StatusCode = 1;
            status.Message = "User has registered successfully.";
            return status;

        }

        Task<Status> IUserAuthentication.LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
