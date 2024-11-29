using System.Collections.Generic;
using UnityEngine.UI;

public interface IPromotionPanelView : IView
{
    /// <summary>
    /// Gets the button used to calculate salaries for selected employees.
    /// </summary>
    Button CalculateSelectedButton { get; }

    /// <summary>
    /// Gets the button used to calculate salaries for all employees.
    /// </summary>
    Button CalculateAllButton { get; }

    /// <summary>
    /// Gets the button used to select all employee cards.
    /// </summary>
    Button SelectAllButton { get; }

    /// <summary>
    /// Gets the button used to clear the selection of employee cards.
    /// </summary>
    Button ClearSelectionButton { get; }

    /// <summary>
    /// Gets the button used to sort employee cards by their ID.
    /// </summary>
    Button SortByIdButton { get; }

    /// <summary>
    /// Gets the button used to sort employee cards by their role.
    /// </summary>
    Button SortByRoleButton { get; }

    /// <summary>
    /// Gets the button used to sort employee cards by their seniority.
    /// </summary>
    Button SortBySeniorityButton { get; }

    /// <summary>
    /// Adds an employee card view to the promotion panel.
    /// </summary>
    /// <param name="employeeCardView">The employee card view to be added.</param>
    void AddEmployeeCardView(IEmployeeCardView employeeCardView);

    /// <summary>
    /// Removes all employee card views from the promotion panel.
    /// </summary>
    void RemoveAllEmployeeCardViews();

    /// <summary>
    /// Binds a button event to a specified action (e.g., when a button is clicked).
    /// </summary>
    /// <param name="button">The button to bind the event to.</param>
    /// <param name="action">The action to perform when the button is clicked.</param>
    void BindButtonEvent(Button button, UnityEngine.Events.UnityAction action);

    /// <summary>
    /// Unbinds a button event from a specified action.
    /// </summary>
    /// <param name="button">The button to unbind the event from.</param>
    /// <param name="action">The action to remove from the button's click event.</param>
    void UnbindButtonEvent(Button button, UnityEngine.Events.UnityAction action);

    /// <summary>
    /// Updates the result text on the view with the old and new salary values.
    /// </summary>
    /// <param name="oldSalary">The old salary value to display.</param>
    /// <param name="newSalary">The new salary value to display.</param>
    void UpdateResultText(float oldSalary, float newSalary);
}