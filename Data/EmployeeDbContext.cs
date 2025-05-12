using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EmployeeManagementSystem.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasQueryFilter(e => e.IsActive);
            modelBuilder.Entity<Department>().HasQueryFilter(d => d.IsActive);

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }



    }//end of class EmployeeDbContext
}//end of namespace EmployeeManagementSystem.Data
