using Azure.Core;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Interfaces;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Enums;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Models;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public UserService(DataContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration= configuration;
        }


        public async Task<UserCreateViewModel> CreateEmployee(UserCreateViewModel employee, int adminId)
        {
            if (employee == null) 
            {
                throw new ArgumentNullException(nameof(employee));
            }

            bool emailExists = await _context.Users.AnyAsync(u => u.Email == employee.Email);

            if (emailExists)
            {
                throw new InvalidOperationException("The employee with this email is already exists.");
            }

            // Get admin data by admin id
            var admin = await _context.Users.FindAsync(adminId);

            // If the administrator with the specified ID is not found, throw an exception
            if (admin == null)
            {
                throw new KeyNotFoundException($"The administrator with ID {adminId} was not found.");
            }

            // The employee's company automatically becomes the company the admin belongs to 
            int? companyId = admin.CompanyId;

            // Password hashing
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(employee.Password);

            var newUser = new User
            {
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Role = Role.Employee,
                CompanyId = companyId,
                Password = passwordHash,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new UserCreateViewModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
            };
        }

        public async Task<List<UserGetViewModel>> GetCompanyEmployees(int companyId)
        {

            var company = await _context.Companys.FindAsync(companyId);

            if (company == null) 
            {
                return null;
            }

            var employees = await _context.Users
                                          .Where(u => u.CompanyId == companyId)
                                          .Select(u => new UserGetViewModel
                                          {
                                              // Mapping properties:
                                              Id = u.Id,
                                              FirstName = u.FirstName,
                                              LastName = u.LastName,
                                              WorkScheduleId = u.WorkScheduleId
                                          })
                                          .ToListAsync();

            return employees;
        }

        public async Task<UserGetViewModel> GetEmployee(int id)
        {
            var employee = await _context.Users.FindAsync(id);

            if (employee == null) 
            {
                return null;
            }

            return new UserGetViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                WorkScheduleId= employee.WorkScheduleId
            };
        }

        public async Task<EmployeeUpdateViewModel> UpdateEmployee(EmployeeUpdateViewModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "The user parameter cannot be null.");
            }

            var employee = await _context.Users.FindAsync(user.Id);

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(user), "The user parameter cannot be null.");
            }

            // Update properties
            employee.Email = user.Email;
            employee.FirstName = user.FirstName;
            employee.LastName = user.LastName;

            _context.Users.Update(employee);
            await _context.SaveChangesAsync();

            return new EmployeeUpdateViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = user.Email,
            };
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Users.FindAsync(id);

            if (employee == null) 
            {
                return false;
            }

            _context.Users.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserCreateViewModel> RegisterAdmin(UserCreateViewModel admin)
        {
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == admin.Email);
            if (emailExists)
            {
                throw new KeyNotFoundException($"The administrator with this email is already exists.");
            }

            // Password hashing
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(admin.Password);

            var newUser = new User
            {
                Email = admin.Email,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Role = Role.Admin,
                Password = passwordHash,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new UserCreateViewModel
            {
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
            };
        }

        #region Login
        public async Task<string> LoginUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return "User Not Found";
            }

            bool checkPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (checkPassword) 
            {
                return GeneretateJWT(user);
            }
            return "Invalid Password";
        }

        private string GeneretateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("companyId", user.CompanyId.ToString() ?? "null"),
                new Claim("role", user.Role.ToString() ?? "null")
            };

            var token = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token); 

            return tokenValue;
        }

        #endregion

        public async Task<string> ChangePassword(ChangePasswordRequestViewModel request)
        {

            int userId = request.userId;

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            string oldPassword = request.oldPassword;
            string newPassword = request.newPassword;

            var checkPassword = BCrypt.Net.BCrypt.Verify(oldPassword, user.Password);

            if (checkPassword)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return "Password Сhanged";
            }

            return "Invalid Old Password";
        }
    }
}
