using System;
using System.Collections.Generic;

public interface IPromotionPanelModel : IModel
{
    /// <summary>
    /// Gets the employee service that provides business logic and employee-related operations.
    /// </summary>
    IEmployeeService EmployeeService { get; }

    /// <summary>
    /// Gets a list of all employee card models in the promotion panel.
    /// </summary>
    List<IEmployeeCardModel> EmployeeCardModels { get; }

    /// <summary>
    /// Gets a list of all employee data in the promotion panel.
    /// </summary>
    public List<EmployeeData> EmployeeDataList { get; }

    /// <summary>
    /// Gets the current selection of employee card models.
    /// </summary>
    IEnumerable<IEmployeeCardModel> CurrentSelection { get; }

    /// <summary>
    /// Adds a new employee card model to the promotion panel.
    /// </summary>
    /// <param name="model">The employee card model to be added.</param>
    void AddEmployeeCardModel(IEmployeeCardModel model);

    /// <summary>
    /// Sorts the employee data based on a custom comparison function.
    /// </summary>
    /// <param name="comparison">A comparison function that determines the sorting order of employee cards.</param>
    void SortEmployeeData(Comparison<IEmployeeCardModel> comparison);

}
