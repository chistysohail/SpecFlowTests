using System.Linq;
using TechTalk.SpecFlow;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SpecFlowTests.Models;
using SpecFlowTests.TestContext;

namespace SpecFlowTests.StepDefinitions
{
    [Binding]
    public class DepartmentSteps
    {
        private readonly TestDbContext _context;
        private Department _retrievedDepartment;

        public DepartmentSteps()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new TestDbContext(options);
        }

        [Given(@"the following departments exist:")]
        public void GivenTheFollowingDepartmentsExist(Table table)
        {
            foreach (var row in table.Rows)
            {
                _context.Departments.Add(new Department
                {
                    DepartmentId = int.Parse(row["DepartmentId"]),
                    Name = row["Name"]
                });
            }
            _context.SaveChanges();
        }

        [Given(@"the following employees exist:")]
        public void GivenTheFollowingEmployeesExist(Table table)
        {
            foreach (var row in table.Rows)
            {
                _context.Employees.Add(new Employee
                {
                    EmployeeId = int.Parse(row["EmployeeId"]),
                    Name = row["Name"],
                    DepartmentId = int.Parse(row["DepartmentId"])
                });
            }
            _context.SaveChanges();
        }

        [Given(@"the following tasks exist:")]
        public void GivenTheFollowingTasksExist(Table table)
        {
            foreach (var row in table.Rows)
            {
                _context.Tasks.Add(new Task
                {
                    TaskId = int.Parse(row["TaskId"]),
                    Description = row["Description"],
                    IsCompleted = bool.Parse(row["IsCompleted"]),
                    EmployeeId = int.Parse(row["EmployeeId"])
                });
            }
            _context.SaveChanges();
        }

        [When(@"I retrieve the department with employees and tasks")]
        public void WhenIRetrieveTheDepartmentWithEmployeesAndTasks()
        {
            _retrievedDepartment = _context.Departments
                .Include(d => d.Employees)
                .ThenInclude(e => e.Tasks)
                .FirstOrDefault();
        }

        [Then(@"I should get the department ""(.*)""")]
        public void ThenIShouldGetTheDepartment(string expectedDepartmentName)
        {
            Assert.IsNotNull(_retrievedDepartment);
            Assert.AreEqual(expectedDepartmentName, _retrievedDepartment.Name);
        }

        [Then(@"it should have (.*) employees")]
        public void ThenItShouldHaveEmployees(int expectedEmployeeCount)
        {
            Assert.AreEqual(expectedEmployeeCount, _retrievedDepartment.Employees.Count);
        }

        [Then(@"""(.*)"" should have (.*) task[s]?")]
        public void ThenEmployeeShouldHaveTasks(string employeeName, int expectedTaskCount)
        {
            var employee = _retrievedDepartment.Employees.FirstOrDefault(e => e.Name == employeeName);
            Assert.IsNotNull(employee);
            Assert.AreEqual(expectedTaskCount, employee.Tasks.Count);
        }

        [Then(@"the total number of tasks should be (.*)")]
        public void ThenTheTotalNumberOfTasksShouldBe(int expectedTaskCount)
        {
            // Count all tasks across all employees in the retrieved department
            int actualTaskCount = _retrievedDepartment.Employees.Sum(e => e.Tasks.Count);

            // Assert the expected task count matches actual count
            Assert.AreEqual(expectedTaskCount, actualTaskCount,
                $"Expected {expectedTaskCount} tasks, but found {actualTaskCount}");
        }


    }
}
