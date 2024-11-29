
public interface IEmployeeCardModel : IModel
{
    /// <summary>
    /// Gets the selection status of the employee card.
    /// </summary>
    bool IsSelected { get; }

    /// <summary>
    /// Gets the unique identifier of the employee.
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Gets the data of the employee.
    /// </summary>
    EmployeeData EmployeeData { get; }

    /// <summary>
    /// Gets the role of the employee.
    /// </summary>
    EmployeeRole EmployeeRole { get; }

    /// <summary>
    /// Gets the seniority level of the employee.
    /// </summary>
    SeniorityLevel SeniorityLevel { get; }

    /// <summary>
    /// Toggles the selection state of the employee card.
    /// </summary>
    void ToggleSelection();

    /// <summary>
    /// Sets the selection state of the employee card.
    /// </summary>
    /// <param name="isSelected">True if the card should be selected, false otherwise.</param>
    void SetSelected(bool isSelected);
}
