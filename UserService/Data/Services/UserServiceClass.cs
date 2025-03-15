using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace UserService.Data.Services
{
    public class UserServiceClass : IUserService
    {
        private readonly AppDbContext _dbcontext;

        public UserServiceClass(AppDbContext context)
        {
            _dbcontext = context;
        }


        public async Task AddNewUser(User user)
        {
            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<string> DeleteUser(int id)
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

        public async Task<User> GetUserById(int id)
        {
            var currentUser = await _dbcontext.Users.FindAsync(id);
            if (currentUser != null)
            {
                return currentUser;
            }
            else 
            {
                return null;
            }
        }

        public async Task UpdateUser(int id, User user)
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
