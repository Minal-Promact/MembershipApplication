using MembershipApplication.Data;
using MembershipApplication.DTO;
using Microsoft.AspNetCore.Identity;

namespace MembershipApplication.Repository
{
    public class UserAuthentication : IUserAuthentication
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserAuthentication(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        //Forgot Password
        public async Task<Status> ForgotPassword(Registration registration)
        {
            var status = new Status();
            ResetPassword resetPassword = new ResetPassword();
            var userDetails = await _userManager.FindByEmailAsync(registration.UserName);
            if (userDetails != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(userDetails);

                resetPassword.Email = registration.UserName;
                resetPassword.Token = token;
                resetPassword.NewPassword = "NayanaChaudhari6490#";
            }
            status.StatusCode = 1;
            status.Message = "User has logined successfully.";
            return status;
        }

        //Login
        public async Task<Status> LoginAsync(LoginModel loginModel)
        {
            var status = new Status();
            //check login 
            var res1 = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, true, false);
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

        //Logout
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //Register User
        public async Task<Status> RegistrationAsync(Registration registration)
        {
            try
            {
                int i = 1;
                var status = new Status();

                

                var userExists = await _userManager.FindByEmailAsync(registration.UserName);
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
                var res = await _userManager.CreateAsync(user, registration.Password);
                if (!res.Succeeded)
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
                if (!await _roleManager.RoleExistsAsync("SUPER_ADMIN"))
                    await _roleManager.CreateAsync(applicationRole);

                if (await _roleManager.RoleExistsAsync("SUPER_ADMIN"))
                {
                    await _userManager.AddToRoleAsync(user, "SUPER_ADMIN");
                }

                //Get UserId
                Int64 userId = Convert.ToInt64(await _userManager.GetUserIdAsync(user));

                ApplicationUser a = await _userManager.FindByEmailAsync(registration.UserName);
                //check role is assigned to user
                bool result = await _userManager.IsInRoleAsync(a, "SUPER_ADMIN");


                //check login 
                if (i == 1)
                {
                    var status1 = new Status();
                    var res1 = await _signInManager.PasswordSignInAsync(registration.UserName, registration.Password, true, false);
                    //Get Record by userId
                    ApplicationUser valUser = await _userManager.FindByIdAsync("2");
                    //Check IsLockedOut
                    bool val = await _userManager.IsLockedOutAsync(valUser);
                    if (res1.Succeeded)
                    {
                        status1.StatusCode = 1;
                        status1.Message = "User has logined successfully.";
                        return status;
                    }
                    status1.StatusCode = 0;
                    status1.Message = "Invalid Username and Password..";
                    return status1;
                }             

                
                //forgot password
                if (i == 3)
                {
                    ResetPassword resetPassword = new ResetPassword();
                    var userDetails = await _userManager.FindByEmailAsync(registration.UserName);
                    if (userDetails != null)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(userDetails);

                        resetPassword.Email = registration.UserName;
                        resetPassword.Token = token;
                        resetPassword.NewPassword = "NayanaChaudhari6490#";
                    }                    
                }

                status.StatusCode = 1;
                status.Message = "User has registered successfully.";
                return status;
            }
            catch (Exception ex)
            {
                var status = new Status();
                status.StatusCode = 0;

                return status;
            }
            finally 
            {
                if(_userManager != null)
                _userManager.Dispose();
            }

        }
        //Reset Password
        public async Task<Status> ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var status = new Status();                
                
                    
                    var userDetails = await _userManager.FindByEmailAsync(resetPassword.Email);
                    if (userDetails != null)
                    {
                        var resetPasswordResult = await _userManager.ResetPasswordAsync(userDetails, resetPassword.Token, resetPassword.NewPassword);
                        if (resetPasswordResult.Succeeded)
                        {
                            status.StatusCode = 1;
                            status.Message = "Password has reset successfully.";
                            return status;
                        }
                    }
                
                //var resreset = await userManager.ChangePasswordAsync(user, registration.Password, "newPassword");

                status.StatusCode = 0;
                status.Message = "Password hasn't reset successfully.";
                return status;

            }
            catch (Exception ex)
            {
                var status = new Status();
                status.StatusCode = 0;
                status.Message = "Password hasn't reset successfully.";
                return status;
            }
        }

        
    }
}
