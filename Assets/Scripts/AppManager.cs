using System.Collections.Generic;
using UnityEngine;

public class AppManager : Singleton<AppManager>
{
    [SerializeField] private Transform _uiRoot;
    [SerializeField] private EmployeeSalaryConfig _salaryConfig;
    [SerializeField] private string _employeeDataCSVPath;

    [SerializeField] private PromotionPanelView _promotionPanelViewPrefab;
    [SerializeField] private EmployeeCardView _employeeCardViewPrefab;

    protected override void Awake()
    {
        base.Awake();
        SetPersistent();
    }
    protected void Start()
    {
        EmployeeRepository employeeRepository = new EmployeeRepository(_employeeDataCSVPath);
        EmployeeService employeeService = new EmployeeService(employeeRepository, _salaryConfig);
        EmployeeCardFactory employeeCardFactory = new EmployeeCardFactory(_employeeCardViewPrefab);
        PromotionPanelFactory promotionPanelFactory = new PromotionPanelFactory(_promotionPanelViewPrefab, employeeCardFactory, employeeService);

        promotionPanelFactory.Create(_uiRoot);
    }
}