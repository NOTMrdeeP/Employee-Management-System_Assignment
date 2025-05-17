// Rohit Aroonslam (s227876075)
// Amity Brown (s227649087)
// Alyssa Damons (s219344892)
// Martin Du Preez (s227147030)
// Marcellos Von Buchenroder (s228139759)

using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        //Default query to filter out IsActive = false Employee records
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Global query filter
            modelBuilder.Entity<Employee>()
                .HasQueryFilter(m => m.IsActive == true);
        }

    }//end of class EmployeeDbContext
}//end of namespace EmployeeManagementSystem.Data
