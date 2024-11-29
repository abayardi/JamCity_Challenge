public interface IEmployeeCardPresenter : IPresenter<IEmployeeCardModel, IEmployeeCardView>
{
    /// <summary>
    /// Initializes the presenter with the given model and view.
    /// </summary>
    /// <param name="model">The model representing the employee card data.</param>
    /// <param name="view">The view that will display the employee card.</param>
    void Initialize(IEmployeeCardModel model, IEmployeeCardView view);
    
    /// <summary>
    /// Sets the model for the presenter.
    /// </summary>
    /// <param name="model"></param>
    void SetModel(IEmployeeCardModel model);

}