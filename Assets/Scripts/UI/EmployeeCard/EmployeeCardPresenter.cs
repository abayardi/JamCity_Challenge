using System;
using UnityEngine;

public class EmployeeCardPresenter : IEmployeeCardPresenter
{
    public IEmployeeCardModel Model { get; set; }
    public IEmployeeCardView View { get; set ; }

    public void Initialize(IEmployeeCardModel model, IEmployeeCardView view)
    {
        Model = model;
        View = view;

        Model.OnModelChanged += UpdateView;
        View.OnToggleChanged += HandleToggleChanged;

        View.UpdateView(Model);
    }

    public void SetModel(IEmployeeCardModel model)
    {
        Model = model;
        UpdateView();
    }

    private void UpdateView()
    {
        View.UpdateView(Model);
    }

    private void HandleToggleChanged(bool isSelected) 
    {
        Model.SetSelected(isSelected);
    }
}

