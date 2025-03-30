using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Data.Services;
using UserService.Model;
using UserService.Model.DTOs;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserAccount _userAccount;

        public UserController(IUserService userService, IUserAccount userAccount)
        {
            _userService = userService;
            _userAccount = userAccount;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(string id)
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
        public async Task<ActionResult<string>> CreateNewUser(USerDTO userdto)
        {
            var response = await _userAccount.CreateAccount(userdto);
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> DeleteUser(string id)
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
        public async Task<ActionResult<string>> UpdateUserById(string id, [FromBody] User newUser)
        {
            await _userService.UpdateUser(id, newUser);
            return Ok(new { message = "user Updated"});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var response = await _userAccount.Login(loginDto);
            return Ok(response);
        }
    }
}
