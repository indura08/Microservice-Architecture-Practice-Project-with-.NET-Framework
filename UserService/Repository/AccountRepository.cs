using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
                UserName = userDto.EmailAddress,
                PasswordHash = userDto.Password,
                Password = userDto.Password
            };

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new ServiceResponse.GeneralResponse(false, "User already registered");

            var createUser = await userManager.CreateAsync(newUser!, userDto.Password);
            if (!createUser.Succeeded)
            {
                var errors = string.Join(", ", createUser.Errors.Select(e => e.Description));
                return new ServiceResponse.GeneralResponse(false, $"An error occured , please try again. Error is : {errors}");
            }

            var checkUser = await roleManager.FindByNameAsync("USER");
            if (checkUser is null) await roleManager.CreateAsync(new IdentityRole() { Name = "USER" });

            await userManager.AddToRoleAsync(newUser, "USER");
            return new ServiceResponse.GeneralResponse(true, "Acccount created successfully");
        }

        public async Task<ServiceResponse.LoginResponse> Login(LoginDTO loginDto)
        {
            if (loginDto is null) return new ServiceResponse.LoginResponse(false, null!, "Login Credentials are not complete , please check again");

            var getUser = await userManager.FindByEmailAsync(loginDto.EmailAddress);
            if (getUser is null) return new ServiceResponse.LoginResponse(false, null!, "User Not found , try again");

            bool checkPassword = await userManager.CheckPasswordAsync(getUser, loginDto.Password);

            if (!checkPassword)
            {
                return new ServiceResponse.LoginResponse(false, null!, "Invalid email/password");
            }

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());

            string token = GenerateToken(userSession);
            return new ServiceResponse.LoginResponse(true, token!, "Login successfull");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role!)
            };

            var Token = new JwtSecurityToken
            (
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
