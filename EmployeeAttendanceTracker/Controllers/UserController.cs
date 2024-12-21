using EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Interfaces;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> RegisterAdmin([FromBody] UserCreateViewModel admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registerAdmin = await _userService.RegisterAdmin(admin);
            return CreatedAtAction(nameof(RegisterAdmin), registerAdmin);
        }

    }
}
