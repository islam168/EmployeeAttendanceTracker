using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Interfaces
{
    public interface ICompanyService
    {
        /// <summary>
        /// Get Company By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CompanyGetUpdateViewModel> GetCompany(int id);

        /// <summary>
        /// Create Company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        Task<CompanyCreateViewModel> CreateCompany(CompanyCreateViewModel company, int adminId);

        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        Task<CompanyGetUpdateViewModel> UpdateCompany(CompanyGetUpdateViewModel company);
    }
}
