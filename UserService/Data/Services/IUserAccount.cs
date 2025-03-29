using UserService.Model.DTOs;

namespace UserService.Data.Services
{
    public interface IUserAccount
    {
        Task<ServiceResponse.GeneralResponse> CreateAccount(USerDTO userDto);
        Task<ServiceResponse.LoginResponse> Login(LoginDTO loginDto);
    }
}
