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
        public DbSet<Company> Companys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region CheckOut
            modelBuilder.Entity<CheckOut>()
                .HasOne(x => x.User)
                .WithMany(x => x.CheckOuts)
                .HasForeignKey(x => x.UserId);
            #endregion

            #region CheckIn
            modelBuilder.Entity<CheckIn>()
                .HasOne(x => x.User)
                .WithMany(x => x.CheckIns)
                .HasForeignKey(x => x.UserId);

            #endregion

            #region User
            modelBuilder.Entity<User>()
                .HasOne(x => x.WorkSchedule)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.WorkScheduleId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CompanyId);
            #endregion


        }
    }
}
