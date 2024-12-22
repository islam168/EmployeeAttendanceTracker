using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get All Company Employees Async
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<List<UserGetViewModel>> GetCompanyEmployees(int companyId);

        /// <summary>
        /// Get Employee By Employee Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserGetViewModel> GetEmployee(int id);

        /// <summary>
        /// Update Employee Async
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns> 
        Task<EmployeeUpdateViewModel> UpdateEmployee(EmployeeUpdateViewModel user);

        /// <summary>
        /// Delete Employee Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteEmployee(int id);

        /// <summary>
        /// Create User Async
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<UserCreateViewModel> CreateEmployee(UserCreateViewModel employee, int adminId);

        /// <summary>
        /// Register Admin Async
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        Task<UserCreateViewModel> RegisterAdmin(UserCreateViewModel admin);

        /// <summary>
        /// Login User Async
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> LoginUser(string email, string password);

        Task<string> ChangePassword(ChangePasswordRequestViewModel requset);
    }
}
