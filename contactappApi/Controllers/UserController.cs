using contactappApi.Models;
using contactappApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace contactappApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetUsers")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }

        [HttpGet("GetUserById")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            var existingUser = _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
           
            _userRepository.UpdateUser(user);
            return Ok(new { id = id });
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            _userRepository.DeleteUser(id);
            return Ok(new { id = id });
        }
    }
}
