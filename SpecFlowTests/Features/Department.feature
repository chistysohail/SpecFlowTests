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
