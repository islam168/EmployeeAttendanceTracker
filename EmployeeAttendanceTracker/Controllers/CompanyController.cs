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
        /// Get Company by Id. Admin Access Only
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompanyById(int id) 
        {
            var company = await _companyService.GetCompany(id);

            if (company == null) 
            {
                return NotFound();
            }

            return Ok(company);
        }

        /// <summary>
        /// Update Company. Admin Access Only
        /// </summary>
        /// <param name="company"></param>
        /// <param name="adminCompanyId"></param>
        /// <returns></returns>
        [HttpPut("{adminCompanyId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyGetUpdateViewModel company, int adminCompanyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the admin can edit this company
            if (company.Id != adminCompanyId)
            {
                return Unauthorized("You are not allowed to update this company.");
            }

            var companyToUpdate = await _companyService.UpdateCompany(company, adminCompanyId);

            if (companyToUpdate == null)
            {
                return NotFound();
            }

            return Ok(companyToUpdate);
        }

        /// <summary>
        /// Create Company. Admin Access Only
        /// </summary>
        /// <param name="company"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
