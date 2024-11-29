using UnityEngine;

public class EmployeeCardFactory : IEmployeeCardFactory
{
    private readonly EmployeeCardView _viewPrefab;

    public EmployeeCardFactory(EmployeeCardView viewPrefab)
    {
        _viewPrefab = viewPrefab;
    }

    public IEmployeeCardPresenter Create((EmployeeData, Transform) args)
    {
        EmployeeCardModel model = new EmployeeCardModel(args.Item1);
        EmployeeCardView view = Object.Instantiate(_viewPrefab, args.Item2);
        EmployeeCardPresenter presenter = new EmployeeCardPresenter();

        presenter.Initialize(model, view);

        return presenter;
    }
}
