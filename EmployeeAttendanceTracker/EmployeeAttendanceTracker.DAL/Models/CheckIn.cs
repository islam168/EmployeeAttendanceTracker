using System.Drawing;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Models
{
    public class CheckIn
    {
        public int Id { get; set; }
        /// <summary>
        /// Time of coming to work
        /// </summary>
        public DateTime CheckInTime { get; set; }
        /// <summary>
        /// Geolocation of the place of confirmation of coming at work (latitude, longitude)
        /// </summary>
        public Point CheckInLocation { get; set; }

        #region Foreign kays
        /// <summary>
        /// Foreign key User.Id - User (Employee) who sent the confirmation of coming to work
        /// </summary>
        public int EmployeeId { get; set; }
        public User User { get; set; }
        #endregion
    }
}
