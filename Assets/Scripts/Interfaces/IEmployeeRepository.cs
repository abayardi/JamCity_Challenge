using UnityEngine;
using System.Collections.Generic;

public interface IEmployeeRepository
{
    /// <summary>
    /// Retrieves all employee data from the repository.
    /// </summary>
    /// <returns>A list of all employees.</returns>
    List<EmployeeData> GetAll();

    /// <summary>
    /// Retrieves an employee's data by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee.</param>
    /// <returns>The employee data if found, otherwise null.</returns>
    EmployeeData GetById(int id);

    /// <summary>
    /// Adds a new employee to the repository.
    /// </summary>
    /// <param name="employee">The employee data to be added.</param>
    void Add(EmployeeData employee);

    /// <summary>
    /// Updates an existing employee's data in the repository.
    /// </summary>
    /// <param name="employee">The employee data to update.</param>
    void Update(EmployeeData employee);

    /// <summary>
    /// Deletes an employee from the repository based on their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Loads employee data from a CSV file and returns a list of employees.
    /// </summary>
    /// <param name="fileName">The name of the CSV file to load employee data from.</param>
    /// <returns>A list of employees loaded from the CSV file.</returns>
    List<EmployeeData> LoadEmployeesFromCSV(string fileName);

}
