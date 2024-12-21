namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Models
{
    public class Company
    {
        public int Id { get; set; }
        /// <summary>
        /// Company name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Latitude of the office where user (employee) works
        /// </summary>
        public decimal CompanyLocationLatitude { get; set; }
        /// <summary>
        /// Longitude of the office where user (employee) works
        /// </summary>
        public decimal CompanyLocationLongitude { get; set; }

        #region Foreign keys
        public ICollection<User> Users { get; set; } = new List<User>();
        #endregion
    }
}
