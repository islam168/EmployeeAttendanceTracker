using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Enums;
using System.Drawing;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// <summary>
        /// Latitude of the user workplace
        /// </summary>
        public double WorkplaceLocationLatitude { get; set; }
        /// <summary>
        /// Longitude of the user workplace
        /// </summary>
        public double WorkplaceLocationLongitude { get; set; }
        /// <summary>
        /// User Role (0: Admin, 1: Employee)
        /// </summary>
        public Role Role { get; set; }
        public string Password { get; set; }

        #region Foreign keys
        /// <summary>
        /// Foreign key WorkSchedule.Id - Employee work schedule
        /// </summary>
        public int WorkScheduleId { get; set; }
        public WorkSchedule WorkSchedule { get; set; }

        public ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

        public ICollection<CheckOut> CheckOuts { get; set; } = new List<CheckOut>();
        #endregion
    }
}
