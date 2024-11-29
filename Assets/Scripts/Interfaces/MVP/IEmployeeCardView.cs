using System;

public interface IEmployeeCardView : IView
{
    /// <summary>
    /// Event triggered when the toggle value changes.
    /// </summary>
    event Action<bool> OnToggleChanged;

    /// <summary>
    /// Updates the view with the given employee card model.
    /// </summary>
    /// <param name="model">The employee card model to display in the view.</param>
    void UpdateView(IEmployeeCardModel model);
}
