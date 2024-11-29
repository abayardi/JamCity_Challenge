using System.Collections.Generic;

public interface IEmployeeService : IService
{
    /// <summary>
    /// Calculates the salary increment for a given employee model based on their role and seniority.
    /// </summary>
    /// <param name="model">The employee card model containing the employee's role and seniority level.</param>
    /// <returns>The calculated salary increment based on the employee's current salary.</returns>
    float CalculateSalaryIncrement(IEmployeeCardModel model);

    /// <summary>
    /// Calculates the current salary for a given employee model based on their role and seniority.
    /// </summary>
    /// <param name="model">The employee card model containing the employee's role and seniority level.</param>
    /// <returns>The current salary of the employee based on predefined salary configurations.</returns>
    float CalculateCurrentSalary(IEmployeeCardModel model);

    /// <summary>
    /// Retrieves all employees managed by the service.
    /// </summary>
    /// <returns>A list of all employees.</returns>
    List<EmployeeData> GetAllEmployees();

    /// <summary>
    /// Retrieves an employee's data by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee.</param>
    /// <returns>The employee data if found; otherwise, null.</returns>
    EmployeeData GetEmployeeById(int id);

    /// <summary>
    /// Adds a new employee to the service.
    /// </summary>
    /// <param name="employee">The employee data to be added to the service.</param>
    void AddEmployee(EmployeeData employee);

    /// <summary>
    /// Updates an existing employee's data in the service.
    /// </summary>
    /// <param name="employee">The employee data to be updated.</param>
    void UpdateEmployee(EmployeeData employee);

    /// <summary>
    /// Sets the salary configuration for the employee service.
    /// </summary>
    /// <param name="config">The salary configuration to be set.</param>
    void SetEmployeeSalaryConfig(EmployeeSalaryConfig config);

    /// <summary>
    /// Deletes an employee from the service based on their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to delete.</param>
    void DeleteEmployee(int id);
}
