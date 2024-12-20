using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region CheckOut
            modelBuilder.Entity<CheckOut>()
                .HasOne(x => x.User)
                .WithMany(x => x.CheckOuts)
                .HasForeignKey(x => x.EmployeeId);
            #endregion

            #region CheckIn
            modelBuilder.Entity<CheckIn>()
                .HasOne(x => x.User)
                .WithMany(x => x.CheckIns)
                .HasForeignKey(x => x.EmployeeId);

            #endregion

            #region WorkSchedule
            modelBuilder.Entity<WorkSchedule>()
                .HasOne(x => x.User)
                .WithOne(x => x.WorkSchedule)
                .HasForeignKey<CheckOut>(x => x.EmployeeId);
            #endregion

            base.OnModelCreating(modelBuilder);

        }
    }
}
