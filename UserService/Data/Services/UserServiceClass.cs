using Microsoft.EntityFrameworkCore;
using UserService.Model;
using UserService.Model.DTOs;

namespace UserService.Data.Services
{
    public class UserServiceClass : IUserService
    {
        private readonly AppDbContext _dbcontext;
        private readonly ILogger<UserServiceClass> _logger;

        public UserServiceClass(AppDbContext context, ILogger<UserServiceClass> logger)
        {
            _dbcontext = context;
            _logger = logger;
        }


        //public async Task AddNewUser(USerDTO userdto)
        //{
        //    _dbcontext.Users.Add(userdto);
        //    await _dbcontext.SaveChangesAsync();
        //}

        public async Task<string> DeleteUser(string id)
        {
            var currentUser = await GetUserById(id);
            if (currentUser != null)
            {
                await _dbcontext.Users.Where(user => user.Id == id).ExecuteDeleteAsync();
                return "Done";
            }
            else 
            {
                return "NotFound";
            }
        }

        public async Task<User> GetUserById(string id)
        {
            _logger.LogInformation($"Get user by id caled with user id : {id}");

            var currentUser = await _dbcontext.Users.FindAsync(id);
            if (currentUser != null)
            {
                _logger.LogWarning($"There is nop user with id {id}");
                return currentUser;
            }
            else 
            {
                return null;
            }
        }

        public async Task UpdateUser(string id, User user)
        {
            var currentUser = await GetUserById(id);
            if (currentUser != null)
            {
                currentUser.EmailAddress = user.EmailAddress;
                currentUser.Name = user.Name;
                currentUser.Password = user.Password;

                _dbcontext.Entry(currentUser).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();

            }
        }
    }
}
