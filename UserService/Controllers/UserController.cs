using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IUserAccount userAccount, ILogger<UserController> logger)
        {
            _userService = userService;
            _userAccount = userAccount;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="USER")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            _logger.LogInformation($"Recieved request to get user by id with id : {id}");

            try
            {
                var currentUser = await _userService.GetUserById(id);
                return Ok(currentUser);
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while fetching data from the server with use id : {id}");
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
