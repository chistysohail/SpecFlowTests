# SpecFlow BDD with Entity Framework Core (EF Core) - .NET 6

## 📌 Project Overview

This project demonstrates **Behavior-Driven Development (BDD)** using **SpecFlow** with **Entity Framework Core (EF Core) 6** and **.NET 6**.

It includes:

- **SpecFlow tests** written in **Gherkin syntax**.
- **Entity Framework Core** using an **In-Memory Database**.
- **Unit Testing** with **NUnit**.
- **.NET 6 migration guide** for easy upgrading.

---

## 🏠 Folder Structure

```
📂 SpecFlowTests
│── 📂 Features
│   ├── Department.feature  # Defines BDD test scenarios
│
│── 📂 StepDefinitions
│   ├── DepartmentSteps.cs  # Implements test steps
│
│── 📂 TestContext
│   ├── TestDbContext.cs  # EF Core In-Memory Database Context
│
│── 📂 Models
│   ├── Department.cs  # Parent entity
│   ├── Employee.cs    # Child entity
│   ├── Task.cs        # Grandchild entity
│
│── SpecFlowTests.csproj  # .NET 6 Project File
│── README.md  # Documentation
```

---

## 🚀 How to Run the Tests

### 1️⃣ Install Dependencies

```sh
dotnet restore
```

### 2️⃣ Build the Project

```sh
dotnet build
```

### 3️⃣ Run SpecFlow Tests

```sh
dotnet test
```

---

## 📝 Feature File: `Department.feature`

This defines a **department-employee-task relationship** and verifies database interactions.

```gherkin
Feature: Department Employee Task Management
  As a system user,
  I want to fetch department data with employees and their tasks,
  So that I can verify the database relationships.

  Scenario: Retrieve department with employees and tasks
    Given the following departments exist:
      | DepartmentId | Name          |
      | 1           | IT Department |
    And the following employees exist:
      | EmployeeId | Name    | DepartmentId |
      | 1         | John Doe | 1            |
      | 2         | Jane Doe | 1            |
    And the following tasks exist:
      | TaskId | Description       | IsCompleted | EmployeeId |
      | 1      | Develop API       | false      | 1          |
      | 2      | Fix UI Bugs       | true       | 1          |
      | 3      | Design Database   | false      | 2          |
    When I retrieve the department with employees and tasks
    Then I should get the department "IT Department"
    And it should have 2 employees
    And "John Doe" should have 2 tasks
    And "Jane Doe" should have 1 task
    And the total number of tasks should be 3
```

---

## 🛠 How to Migrate from .NET Core 3.1 to .NET 6

### **1️⃣ Update **``

Change the **target framework** to `.NET 6` inside `SpecFlowTests.csproj`:

```xml
<PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
</PropertyGroup>
```

Run:

```sh
dotnet restore
dotnet build
```

---

### **2️⃣ Update **``** for .NET 6**

Modify `TestDbContext.cs` to **use constructor injection**:

```csharp
public class TestDbContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Task> Tasks { get; set; }

    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }
}
```

---

### **3️⃣ Update **``** for .NET 6**

Modify the **SpecFlow step definition** to **use dependency injection**:

```csharp
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
}
```

---

## 🛡 Final Steps to Run .NET 6 Version

After migration, run:

```sh
dotnet clean
dotnet build
dotnet test
```

If everything is correct, all **SpecFlow tests should pass! ✅**

---

## 🚀 Key Benefits of Migrating to .NET 6

✔ **Performance improvements** in EF Core 6\
✔ **Better Dependency Injection support**\
✔ **Long-Term Support (LTS) until 2024**\
✔ **Simplified code with **``** constructor injection**

---

## 📌 Troubleshooting Common Issues

| Issue                           | Solution                                                   |
| ------------------------------- | ---------------------------------------------------------- |
| Tests not running               | Run `dotnet test --logger console;verbosity=detailed`      |
| Feature file not recognized     | Right-click `.feature` file → "Regenerate Feature Files"   |
| No step definition found        | Ensure **step definition regex matches feature file text** |
| Database reset on each test run | **Use **`` inside `DbContextOptionsBuilder`                |

---

## 🚀 Conclusion

- **SpecFlow BDD testing** provides **clear and human-readable tests**.
- **Entity Framework Core (EF Core) In-Memory Database** ensures **fast testing**.
- **Migrating to .NET 6** improves **performance and maintainability**.

---

### 🔗 Useful Links:

- [SpecFlow Official Docs](https://specflow.org/)
- [EF Core 6 Docs](https://learn.microsoft.com/en-us/ef/core/)

---

### 🎯 Now You’re Ready to Run SpecFlow BDD Tests in .NET 6! 🚀

