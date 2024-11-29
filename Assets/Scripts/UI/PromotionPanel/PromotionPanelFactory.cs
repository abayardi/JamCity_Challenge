using System.Collections.Generic;
using UnityEngine;

public class PromotionPanelFactory : IPromotionPanelFactory
{
    private readonly PromotionPanelView _viewPrefab;
    private readonly IEmployeeCardFactory _employeeCardFactory;
    private readonly IEmployeeService _employeeService;

    public PromotionPanelFactory(PromotionPanelView viewPrefab, IEmployeeCardFactory employeeCardFactory, IEmployeeService employeeService)
    {
        _viewPrefab = viewPrefab;
        _employeeCardFactory = employeeCardFactory;
        _employeeService = employeeService;
    }

    public IPromotionPanelPresenter Create(Transform parent)
    {
        List<EmployeeData> employeeDatas = _employeeService.GetAllEmployees();
        PromotionPanelModel model = new PromotionPanelModel(_employeeService, employeeDatas);
        PromotionPanelView view = Object.Instantiate(_viewPrefab, parent);
        PromotionPanelPresenter presenter = new PromotionPanelPresenter(_employeeCardFactory);

        presenter.Initialize(model, view);

        return presenter;
    }
}
