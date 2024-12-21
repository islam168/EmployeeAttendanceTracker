using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels
{
    /// <summary>
    /// ViewModel base to prevent code repetition 
    /// </summary>
    public abstract class CheckInBaseViewModel
    {
        /// <summary>
        /// Time of coming to work
        /// </summary>
        public DateTime CheckInTime { get; set; }
        /// <summary>
        /// Latitude of the location of the confirmation of arrival at work.
        /// Range from -90° to 90°, which corresponds to the possible range of latitude on the globe.
        /// </summary>
        [Range(-90, 90)]
        public decimal Latitude { get; set; }
        /// <summary>
        /// Longitude of the location of the confirmation of arrival at work.
        /// Range from -180° to 180°, which corresponds to the possible range of longitude on the globe.
        /// </summary>
        [Range(-180, 180)]
        public decimal Longitude { get; set; }
        /// <summary>
        /// User ID that the check in belongs to.
        /// </summary>
        public int UserId { get; set; }
    }
    /// <summary>
    /// ViewModel to create check in
    /// </summary>
    public class CheckInCreateViewModel : CheckInBaseViewModel { }
    /// <summary>
    /// ViewModel to get check in data
    /// </summary>
    public class CheckInGetViewModel : CheckInBaseViewModel
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }
    }
}
