namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Models
{
    public class CheckOut
    {
        public int Id { get; set; }
        /// <summary>
        /// Time of leaving to work
        /// </summary>
        public DateTime CheckOutTime { get; set; }
        /// <summary>
        /// Latitude of the place of confirmation of leaving at work
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// Longitude of the place of confirmation of leaving at work
        /// </summary>
        public decimal Longitude { get; set; }

        #region Foreign kays
        /// <summary>
        /// Foreign key User.Id - User (Employee) who sent the confirmation of leaving for work
        /// </summary>
        public int UserId { get; set; }
        public User User { get; set; }
        #endregion
    }
}
