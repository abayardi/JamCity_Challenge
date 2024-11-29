using System.Collections.Generic;

public interface IPromotionPanelPresenter : IPresenter<IPromotionPanelModel, IPromotionPanelView>
{
    /// <summary>
    /// Initializes the presenter with the provided model, view, and employee data.
    /// </summary>
    void Initialize(IPromotionPanelModel model, IPromotionPanelView view);

    /// <summary>
    /// Disposes the presenter, cleaning up resources and view.
    /// </summary>
    void Dispose();

    /// <summary>
    /// Calculates salaries for the provided list of employee card models.
    /// </summary>
    void CalculateSalaries(IEnumerable<IEmployeeCardModel> employeeCardModels);

    /// <summary>
    /// Updates the selection state of all employee cards.
    /// </summary>
    void UpdateAllEmployeeSelections(bool select);

    /// <summary>
    /// Updates the view with the latest data from the model.
    /// </summary>
    void UpdateView();

}
