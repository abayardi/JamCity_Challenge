using System;
using System.Collections.Generic;

public class PromotionPanelPresenter : IPromotionPanelPresenter
{
    public IPromotionPanelModel Model { get; set; }
    public IPromotionPanelView View { get; set; }

    private readonly List<IEmployeeCardPresenter> _employeeCardPresenters = new();

    private readonly IEmployeeCardFactory _employeeCardFactory;

    public PromotionPanelPresenter(IEmployeeCardFactory employeeCardFactory)
    {
        _employeeCardFactory = employeeCardFactory;
    }

    public void Initialize(IPromotionPanelModel model, IPromotionPanelView view)
    {
        Model = model;
        View = view;

        Model.OnModelChanged += UpdateView;

        BindView();
        InitializeEmployeeCards();
    }

    public void Dispose()
    {
        UnbindView();
        Model.OnModelChanged -= UpdateView;
        View.RemoveAllEmployeeCardViews();
        Model = null;
        View = null;
    }

    public void CalculateSalaries(IEnumerable<IEmployeeCardModel> employeeCardModels)
    {
        float oldSalaryResult = 0;
        float newSalaryResult = 0;

        foreach (var employeeCardModel in employeeCardModels)
        {
            oldSalaryResult += Model.EmployeeService.CalculateCurrentSalary(employeeCardModel);
            newSalaryResult += Model.EmployeeService.CalculateSalaryIncrement(employeeCardModel);
        }

        View.UpdateResultText(oldSalaryResult, newSalaryResult);
    }

    public void UpdateAllEmployeeSelections(bool select)
    {
        foreach (IEmployeeCardModel employeeCardModel in Model.EmployeeCardModels)
        {
            employeeCardModel.SetSelected(select);
        }
        Model.NotifyChange();
    }

    public void UpdateView()
    {
        for(int i = 0; i < _employeeCardPresenters.Count; i++)
        {
            _employeeCardPresenters[i].SetModel(Model.EmployeeCardModels[i]);
        }
    }

    private void InitializeEmployeeCards()
    {
        foreach (var employeeData in Model.EmployeeDataList)
        {
            var employeeCardPresenter = _employeeCardFactory.Create((employeeData,View.Transform));
            _employeeCardPresenters.Add(employeeCardPresenter);
            View.AddEmployeeCardView(employeeCardPresenter.View);
            Model.AddEmployeeCardModel(employeeCardPresenter.Model);
        }
    }

    private void BindView()
    {
        View.BindButtonEvent(View.CalculateSelectedButton, OnCalculateSelectedClicked);
        View.BindButtonEvent(View.CalculateAllButton, OnCalculateAllClicked);
        View.BindButtonEvent(View.SelectAllButton, OnSelectAllClicked);
        View.BindButtonEvent(View.ClearSelectionButton, OnClearSelectionClicked);
        View.BindButtonEvent(View.SortByIdButton, SortById);
        View.BindButtonEvent(View.SortByRoleButton, SortByRole);
        View.BindButtonEvent(View.SortBySeniorityButton, SortBySeniority);
    }

    private void UnbindView()
    {
        View.UnbindButtonEvent(View.CalculateSelectedButton, OnCalculateSelectedClicked);
        View.UnbindButtonEvent(View.CalculateAllButton, OnCalculateAllClicked);
        View.UnbindButtonEvent(View.SelectAllButton, OnSelectAllClicked);
        View.UnbindButtonEvent(View.ClearSelectionButton, OnClearSelectionClicked);
        View.UnbindButtonEvent(View.SortByIdButton, SortById);
        View.UnbindButtonEvent(View.SortByRoleButton, SortByRole);
        View.UnbindButtonEvent(View.SortBySeniorityButton, SortBySeniority);
    }

    private void SortById()
    {
        Model.SortEmployeeData((x, y) => x.Id.CompareTo(y.Id));
    }

    private void SortByRole()
    {
        Model.SortEmployeeData((x, y) => x.EmployeeRole.CompareTo(y.EmployeeRole));
    }

    private void SortBySeniority()
    {
        Model.SortEmployeeData((x, y) => x.SeniorityLevel.CompareTo(y.SeniorityLevel));
    }

    private void OnCalculateSelectedClicked()
    {
        CalculateSalaries(Model.CurrentSelection);
    }

    private void OnCalculateAllClicked()
    {
        CalculateSalaries(Model.EmployeeCardModels);
    }

    private void OnSelectAllClicked()
    {
        UpdateAllEmployeeSelections(true);
    }

    private void OnClearSelectionClicked()
    {
        UpdateAllEmployeeSelections(false);
    }

}
