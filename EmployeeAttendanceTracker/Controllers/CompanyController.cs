using EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Interfaces;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAttendanceTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService) 
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Create Company
        /// </summary>
        /// <param name="company"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateViewModel company, int adminId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createCompany = await _companyService.CreateCompany(company, adminId);
            return CreatedAtAction(nameof(CreateCompany), createCompany);
        }
    }
}
