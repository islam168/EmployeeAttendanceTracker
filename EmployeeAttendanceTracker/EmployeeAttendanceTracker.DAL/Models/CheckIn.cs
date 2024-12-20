﻿using System.Drawing;

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
        /// Latitude of the location of the confirmation of arrival at work
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude of the location of the confirmation of arrival at work
        /// </summary>
        public double Longitude { get; set; }


        #region Foreign kays
        /// <summary>
        /// Foreign key User.Id - User (Employee) who sent the confirmation of coming to work
        /// </summary>
        public int EmployeeId { get; set; }
        public User User { get; set; }
        #endregion
    }
}
