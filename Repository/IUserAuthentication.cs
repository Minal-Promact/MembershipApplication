using MembershipApplication.DTO;

namespace MembershipApplication.Repository
{
    public interface IUserAuthentication
    {
        Task<Status> RegistrationAsync(Registration registration);        
        Task<Status> LoginAsync(LoginModel loginModel);
        Task<Status> LogoutAsync();
        
    }
}
