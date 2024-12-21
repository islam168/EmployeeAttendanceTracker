using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels
{
    /// <summary>
    /// ViewModel base to prevent code repetition 
    /// </summary>
    public abstract class CompanyBaseViewModel
    {
        /// <summary>
        /// Company name
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Latitude of the office where user (employee) works.
        /// Range from -90° to 90°, which corresponds to the possible range of latitude on the globe.
        /// </summary>
        [Range(-90, 90)]
        public decimal CompanyLocationLatitude { get; set; }
        /// <summary>
        /// Longitude of the office where user (employee) works
        /// Range from -180° to 180°, which corresponds to the possible range of longitude on the globe.
        /// </summary>
        [Range(-180, 180)]
        public decimal CompanyLocationLongitude { get; set; }
    }
    /// <summary>
    /// ViewModel to create company data
    /// </summary>
    public class CompanyCreateViewModel : CompanyBaseViewModel {  }
    /// <summary>
    /// ViewModel to get and update company data
    /// </summary>
    public class CompanyGetUpdateViewModel : CompanyBaseViewModel 
    {
        /// <summary>
        /// Company Primary Key
        /// </summary>
        public int Id { get; set; }
    }
}
