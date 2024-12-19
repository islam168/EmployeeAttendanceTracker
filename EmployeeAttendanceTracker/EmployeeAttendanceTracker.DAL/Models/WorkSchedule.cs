namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Models
{
    public class WorkSchedule
    {
        public int Id { get; set; }
        /// <summary>
        /// Employee days of work (0: Monday, 1: Tuesday, 2: Wednesday, 
        /// 3: Thursday, 4: Friday, 5: Saturday, 6: Sunday)
        /// </summary>
        public DayOfWeek WorkDay { get; set; }
        /// <summary>
        /// Employee start time
        /// </summary>
        public TimeOnly WorkStartTime { get; set; }
        /// <summary>
        /// Employee end time
        /// </summary>
        public TimeOnly WorkEndTime { get; set; }
        /// <summary>
        /// Date and time when the work schedule was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Date and time of the last work schedule update
        /// </summary>
        public DateTime LastUpdatedAt { get; set; }

        #region Foreign keys
        /// <summary>
        /// Foreign key User.Id - User (Employee) who has a work schedule
        /// </summary>
        public int EmployeeId { get; set; }
        public User User { get; set; }
        #endregion
    }
}
