using UserService.Model;

namespace UserService.Data.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task AddNewUser(User user);
        Task UpdateUser(int id, User user);
        Task<string> DeleteUser(int id);
    }
}
