using TechConnect.DtoUI.IdentityDtos.LoginDtos;

namespace TechConnect.WebUI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> SignIn(SignInDto signInDto);
        Task<bool> GetRefreshToken();
    }
}
