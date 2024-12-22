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

        public async Task<CompanyGetUpdateViewModel> GetCompany(int id)
        {
            var company = await _context.Companys.FindAsync(id);

            if (company == null) 
            {
                return null;
            }

            return new CompanyGetUpdateViewModel
            {
                Id = company.Id,
                Name = company.Name, 
                CompanyLocationLatitude = company.CompanyLocationLatitude,
                CompanyLocationLongitude = company.CompanyLocationLongitude,
            };
        }

        public async Task<CompanyGetUpdateViewModel> UpdateCompany(CompanyGetUpdateViewModel company, int adminCompanyId)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company), "The company parameter cannot be null.");
            }

            var companyById = await _context.Companys.FindAsync(company.Id);

            if (companyById == null)
            {
                return null;
            }

            // Update properties
            companyById.Name = company.Name;
            companyById.CompanyLocationLatitude = company.CompanyLocationLatitude;
            companyById.CompanyLocationLongitude = company.CompanyLocationLongitude;

            _context.Companys.Update(companyById);
            await _context.SaveChangesAsync();

            return new CompanyGetUpdateViewModel
            {
                Id = companyById.Id,
                Name = companyById.Name,
                CompanyLocationLatitude = companyById.CompanyLocationLatitude,
                CompanyLocationLongitude = companyById.CompanyLocationLongitude
            };
        }
    }
}
