using System.Collections.Generic;

namespace SpecFlowTests.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        // Foreign key for Department
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // Navigation property for Tasks
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}