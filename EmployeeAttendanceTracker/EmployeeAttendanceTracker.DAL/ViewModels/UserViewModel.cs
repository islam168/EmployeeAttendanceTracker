using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels
{
    /// <summary>
    /// ViewModel base to prevent code repetition 
    /// </summary>
    public abstract class UserBaseViewModel
    {
        /// <summary>
        /// User mail format validation
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// The maximum length of the last user name is 50 characters
        /// </summary>
        [StringLength(50)]
        public string FirstName { get; set; }
        /// <summary>
        /// The maximum length of the last user name is 50 characters
        /// </summary>
        [StringLength(50)]
        public string LastName { get; set; }
        /// <summary>
        /// User Role (0: Admin, 1: Employee)
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// Foreign key WorkSchedule.Id - Employee work schedule
        /// </summary>
        public int WorkScheduleId { get; set; }
        /// <summary>
        /// Foreign key Company.Id - User company
        /// </summary>
        public int CompanyId { get; set; }

    }
    /// <summary>
    /// ViewModel for user creation (registration)
    /// </summary>
    public class UserCreateViewModel : UserBaseViewModel
    {
        /// <summary>
        /// Checking the password for a minimum of 8 characters
        /// </summary>
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }

    /// <summary>
    /// ViewModel to get user data
    /// </summary>
    public class UserGetViewModel : UserBaseViewModel
    {
        /// <summary>
        /// User Primary Key
        /// </summary>
        public int Id { get; set; }
    }
    /// <summary>
    /// ViewModel to update user data
    /// </summary>
    public class UserUpdateViewModel : UserBaseViewModel
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Checking the password for a minimum of 8 characters
        /// </summary>
        [MinLength(8)]
        public string Password { set; get; }
    }

    /// <summary>
    /// ViewModel for user login
    /// </summary>
    public class UserLoginViewModel
    {
        /// <summary>
        /// User mail format validation
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Checking the password for a minimum of 8 characters.
        /// </summary>
        [Required]
        [MinLength(8)]
        public string Password {  set; get; }
    }
}
