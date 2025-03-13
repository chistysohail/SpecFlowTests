namespace SpecFlowTests.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        // Foreign key for Employee
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}