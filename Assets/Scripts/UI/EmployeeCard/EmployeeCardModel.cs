using System;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeCardModel : IEmployeeCardModel
{
    private bool _isSelected = false;
    public bool IsSelected => _isSelected;

    private readonly EmployeeData _data;
    public EmployeeData EmployeeData { get => _data; }

    public event Action OnModelChanged;

    public int Id => _data.Id;
    public EmployeeRole EmployeeRole => _data.EmployeeRole;
    public SeniorityLevel SeniorityLevel => _data.SeniorityLevel;

    public EmployeeCardModel(EmployeeData data)
    {
        _data = data;
    }

    public void ToggleSelection()
    {
        _isSelected = !IsSelected;
        NotifyChange();
    }

    public void SetSelected(bool isSelected)
    {
        if (IsSelected != isSelected)
        {
            _isSelected = isSelected;
            NotifyChange();
        }
    }

    public void NotifyChange()
    {
        OnModelChanged?.Invoke();
    }
}
