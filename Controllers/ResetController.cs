using MembershipApplication.DTO;
using MembershipApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MembershipApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResetController : Controller
    {
        private readonly IUserAuthentication _userAuthentication;

        public ResetController(IUserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        //Register User data into asp.net user
        [HttpPost]
        public async Task<IActionResult> ResetAsync(ResetPassword resetPassword)
        {
            var status = await _userAuthentication.ResetPassword(resetPassword);
            return (IActionResult)status;
        }
    }
}
