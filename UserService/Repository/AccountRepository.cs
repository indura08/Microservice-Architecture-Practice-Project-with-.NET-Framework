using Microsoft.AspNetCore.Identity;
using UserService.Data.Services;
using UserService.Model;
using UserService.Model.DTOs;

namespace UserService.Repository
{
    public class AccountRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IUserAccount
    {
        public async Task<ServiceResponse.GeneralResponse> CreateAccount(USerDTO userDto)
        {
            if (userDto is null) return new ServiceResponse.GeneralResponse(false, "Model is empty, try again");

            var newUser = new User
            {
                Name = userDto.Name,
                EmailAddress = userDto.EmailAddress,
                Email = userDto.EmailAddress,
                PasswordHash = userDto.Password,
                Password = userDto.Password
            };

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new ServiceResponse.GeneralResponse(false, "User already registered");

            var createUser = await userManager.CreateAsync(newUser!, userDto.Password);

            return null;
        }

        public Task<ServiceResponse.LoginResponse> Login(LoginDTO loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
