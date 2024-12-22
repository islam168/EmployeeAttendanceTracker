using Azure;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Interfaces;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeAttendanceTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Admin Registration
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        [HttpPost("admin-registration")]
        [Authorize]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserCreateViewModel admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registerAdmin = await _userService.RegisterAdmin(admin);
            return CreatedAtAction(nameof(RegisterAdmin), registerAdmin);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.LoginUser(user.Email, user.Password);

            if (result == "Invalid Password")
                return Unauthorized(result);

            HttpContext.Response.Cookies.Append("secretCookies", result);

            //This will add the token to response body
            return Ok(result);
        }
    }
}
