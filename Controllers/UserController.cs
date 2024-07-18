using AutoMapper;
using EventManagementAPICRUD.DTO_s;
using EventManagementAPICRUD.Models;
using EventManagementAPICRUD.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPICRUD.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                User user = _mapper.Map<User>(userDTO);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                var createdUser = await _repository.Register(user);
                return Ok("User register Successfully");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var user = await _repository.Login(login.Username, login.PasswordHash);
            if (string.IsNullOrEmpty(user))
                return NotFound();

            // Generate JWT token if necessary
            return Ok(user);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            return Ok("Logged out successfully.");
        }


    }
}