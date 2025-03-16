using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Data.Services;
using UserService.Model;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var currentUser = await _userService.GetUserById(id);
            if (currentUser != null)
            {
                return Ok(currentUser);
            }
            else 
            {
                return NotFound(null);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateNewUser(User user)
        {
            await _userService.AddNewUser(user);
            return Ok("user created successfully");
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var status = await _userService.DeleteUser(id);
            if (status.ToString() == "Done")
            {
                return Ok("user deleted");
            }
            else 
            {
                return NotFound($"user not found with id : {id}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<string>> UpdateUserById(int id, [FromBody] User newUser)
        {
            await _userService.UpdateUser(id, newUser);
            return Ok(new { message = "user Updated"});
        }
    }
}
