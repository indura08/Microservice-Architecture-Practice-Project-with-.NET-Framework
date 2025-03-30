using UserService.Model;
using UserService.Model.DTOs;

namespace UserService.Data.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(string id);
        //Task AddNewUser(USerDTO userdto);
        Task UpdateUser(string id, User user);
        Task<string> DeleteUser(string id);
    }
}
