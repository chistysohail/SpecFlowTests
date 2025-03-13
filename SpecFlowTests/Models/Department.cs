using System.Collections.Generic;

namespace SpecFlowTests.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        // Navigation property for Employees
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}