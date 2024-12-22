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
        /// Get the employees who work for the company  
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet("companyEmployee/{companyId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompanyEmployees(int companyId)
        {
            var employees = await _userService.GetCompanyEmployees(companyId);

            if (employees == null) 
            {
                return NotFound("The company with this id does not exist");
            }

            return Ok(employees);
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("employee/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _userService.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        /// <summary>
        /// Create Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost("createEmployee")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee([FromBody] UserCreateViewModel employee, int adminId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createEmployee = await _userService.CreateEmployee(employee, adminId);
            return CreatedAtAction(nameof(CreateEmployee), createEmployee);
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("updateEmployee{employeeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeUpdateViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeUpdate = await _userService.UpdateEmployee(employee);

            if (employeeUpdate == null)
            {
                return NotFound();
            }

            return Ok(employeeUpdate);
        }

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deleteEmployee{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeeleteEmployee(int id)
        {
            bool employee = await _userService.DeleteEmployee(id);

            if (!employee) 
            {
                return NotFound();
            }

            return NoContent();
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

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.LoginUser(user.Email, user.Password);

            if (result == "Invalid Password")
            {
                return Unauthorized(result);
            }

            else if (result == "User Not Found")
            {
                return NotFound("User Not Found");
            }

            HttpContext.Response.Cookies.Append("secretCookies", result);

            //This will add the token to response body
            return Ok(result);
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestViewModel request)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            // Checking for a match of sent passwords
            if (request.oldPassword == request.newPassword) 
            {
                return BadRequest("The passwords entered match");
            }

            var result = await _userService.ChangePassword(request);

            if (result == null)
            {
                return NotFound("User Not Found");
            }

            if (result == "Password Сhanged")
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
