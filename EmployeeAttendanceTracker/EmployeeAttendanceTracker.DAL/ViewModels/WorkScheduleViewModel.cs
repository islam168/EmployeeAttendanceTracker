using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.ViewModels
{
    /// <summary>
    /// ViewModel base to prevent code repetition 
    /// </summary>
    public abstract class WorkScheduleBaseViewModel
    {
        /// <summary>
        /// Employee days of work (0: Monday, 1: Tuesday, 2: Wednesday, 
        /// 3: Thursday, 4: Friday, 5: Saturday, 6: Sunday)
        /// </summary>
        [Range(0, 6, ErrorMessage = "WorkDay must be between 0 (Monday) and 6 (Sunday).")]
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
        /// User ID that the schedule belongs to.
        /// </summary>
        public int UserId { get; set; }
    }
    /// <summary>
    /// ViewModel to create work schedule
    /// </summary>
    public class WorkScheduleCreateViewModel : WorkScheduleBaseViewModel 
    {
        /// <summary>
        /// Date and time when the work schedule was created.
        /// </summary>
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    }
    /// <summary>
    /// ViewModel to update work schedule data
    /// </summary>
    public class WorkScheduleUpdateViewModel : WorkScheduleBaseViewModel
    {
        /// <summary>
        /// WorkSchedule Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date and time of the last work schedule update.
        /// </summary>
        public DateTime LastUpdatedAt { get; private set; } = DateTime.UtcNow;

    }

    /// <summary>
    /// ViewModel to get work schedule data
    /// </summary>
    public class WorkScheduleGetViewModel : WorkScheduleBaseViewModel
    {
        /// <summary>
        /// WorkSchedule Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date and time when the work schedule was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Date and time of the last work schedule update
        /// </summary>
        public DateTime LastUpdatedAt { get; set; }
    }
}
