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


        public async Task<UserCreateViewModel> CreateUser(UserCreateViewModel user, int adminId)
        {
            if (user == null) 
            {
                throw new ArgumentNullException(nameof(user));
            }

            bool emailExists = await _context.Users.AnyAsync(u => u.Email == user.Email);

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
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var newUser = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = Role.Employee,
                CompanyId = companyId,
                Password = passwordHash,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new UserCreateViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
        }

        public Task<bool> DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserGetViewModel>> GetCompanyEmployees(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<UserGetViewModel> GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserUpdateViewModel> UpdateEmployee(UserUpdateViewModel user)
        {
            throw new NotImplementedException();
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
                throw new KeyNotFoundException("User Not Found");

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
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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
    }
}
