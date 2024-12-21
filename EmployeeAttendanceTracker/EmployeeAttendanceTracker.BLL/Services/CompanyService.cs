using EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Interfaces;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Models;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DataContext _context;
        public CompanyService(DataContext context) 
        {
            _context = context;
        }

        public async Task<CompanyCreateViewModel> CreateCompany(CompanyCreateViewModel company, int adminId)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            var admin = await _context.Users.FindAsync(adminId);
            if (admin == null)
            {
                throw new KeyNotFoundException($"Administrator with ID {adminId} not found.");
            }
            // Check if the admin has a company tied to the account. Admin cannot create more than 1 company
            if (admin.CompanyId != null) 
            {
                throw new InvalidOperationException("The admin already has a company.");
            }

            var newCompany = new Company
            {
                Name = company.Name,
                CompanyLocationLatitude = company.CompanyLocationLatitude,
                CompanyLocationLongitude = company.CompanyLocationLongitude,
            };

            _context.Companys.Add(newCompany);
            await _context.SaveChangesAsync();
            var com = _context.Companys.FirstOrDefault(x => x.Id == newCompany.Id);

            admin.CompanyId = newCompany.Id;

            _context.Users.Update(admin);
            await _context.SaveChangesAsync();

            return new CompanyCreateViewModel
            {
                Name = company.Name,
                CompanyLocationLatitude = company.CompanyLocationLatitude,
                CompanyLocationLongitude = company.CompanyLocationLongitude,
            };
        }

        public Task<CompanyGetUpdateViewModel> GetCompany(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyGetUpdateViewModel> UpdateCompany(CompanyGetUpdateViewModel company)
        {
            throw new NotImplementedException();
        }
    }
}
