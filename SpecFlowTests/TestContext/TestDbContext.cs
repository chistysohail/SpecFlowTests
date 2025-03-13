using Microsoft.EntityFrameworkCore;
using SpecFlowTests.Models;

namespace SpecFlowTests.TestContext
{
    public class TestDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define Department → Employees (One-to-Many)
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);  // Ensures cascading delete

            // Define Employee → Tasks (One-to-Many)
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Tasks)
                .WithOne(t => t.Employee)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);  // Ensures cascading delete
        }
    }
}