using MembershipApplication.DTO;
using MembershipApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MembershipApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IUserAuthentication _userAuthentication;

        public HomeController(IUserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        //Register User data into asp.net user
        [HttpPost]
        public async Task<IActionResult> PostAsync(Registration registration)
        {
            var status = await _userAuthentication.RegistrationAsync(registration);
            return RedirectToAction();
        }

        

    }
}

           

