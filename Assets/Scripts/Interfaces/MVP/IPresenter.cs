
/// <summary>
/// Generic interface for a controller in the MVC pattern.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TView">The type of the view.</typeparam>
public interface IPresenter<TModel, TView>
    where TModel : IModel
    where TView : IView
{
    TModel Model { get; set; }
    TView View { get; set; }
}