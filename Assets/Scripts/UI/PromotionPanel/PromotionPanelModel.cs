using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class PromotionPanelModel : IPromotionPanelModel
{
    private IEmployeeService _employeeService;
    public IEmployeeService EmployeeService => _employeeService;

    private List<IEmployeeCardModel> _employeeCardModels = new List<IEmployeeCardModel>();
    public List<IEmployeeCardModel> EmployeeCardModels => _employeeCardModels;
    public IEnumerable<IEmployeeCardModel> CurrentSelection => EmployeeCardModels.Where(card => card.IsSelected);

    public event Action OnModelChanged;

    private List<EmployeeData> _employeeDataList = new List<EmployeeData>();
    public List<EmployeeData> EmployeeDataList => _employeeDataList;

    public PromotionPanelModel(IEmployeeService employeeService, List<EmployeeData> employeeDataList) 
    {
        _employeeService = employeeService;
        _employeeDataList = employeeDataList;
    }
    
    public void NotifyChange()
    {
        OnModelChanged?.Invoke();
    }

    public void AddEmployeeCardModel(IEmployeeCardModel cardModel)
    {
        _employeeCardModels.Add(cardModel);
    }

    public void RemoveEmployeeCardModel(IEmployeeCardModel cardModel)
    {
        _employeeCardModels.Remove(cardModel);
    }

    public void SortEmployeeData(Comparison<IEmployeeCardModel> comparison)
    {
        _employeeCardModels.Sort(comparison);
        _employeeDataList = _employeeCardModels.Select(card => _employeeDataList.FirstOrDefault(data => data.Id == card.Id)).ToList();

        NotifyChange();
    }

}
