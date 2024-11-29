using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Transform = UnityEngine.Transform;


/// <summary>
/// Implementation of the view for the Promotion Panel in the MVC pattern.
/// </summary>
public class PromotionPanelView : MonoBehaviour, IPromotionPanelView
{
    public Transform Transform => transform;
    public GameObject GameObject => gameObject;

    [SerializeField]
    private Button _calculateSelectedButton;
    public Button CalculateSelectedButton => _calculateSelectedButton;

    [SerializeField]
    private Button _calculateAllButton;
    public Button CalculateAllButton => _calculateAllButton;

    [SerializeField]
    private Button _selectAllButton;
    public Button SelectAllButton => _selectAllButton;

    [SerializeField]
    private Button _clearSelectionButton;
    public Button ClearSelectionButton => _clearSelectionButton;

    [SerializeField]
    private Button _sortByIdButton;
    public Button SortByIdButton => _sortByIdButton;

    [SerializeField]
    private Button _sortByRoleButton;
    public Button SortByRoleButton => _sortByRoleButton;

    [SerializeField]
    private Button _sortBySeniorityButton;
    public Button SortBySeniorityButton => _sortBySeniorityButton;

    [SerializeField]
    private TMP_Text _oldSalaryText;

    [SerializeField]
    private TMP_Text _newSalaryText;

    [SerializeField]
    private Transform _employeeListContainer;

    private List<IEmployeeCardView> _currentEmployeeCardViews = new List<IEmployeeCardView>();

    public void AddEmployeeCardView(IEmployeeCardView employeeCardView)
    {
        _currentEmployeeCardViews.Add(employeeCardView);
        employeeCardView.Transform.SetParent(_employeeListContainer);
    }

    public void RemoveEmployeeCardView(IEmployeeCardView employeeCardView)
    {
        _currentEmployeeCardViews.Remove(employeeCardView);
        Destroy(employeeCardView.GameObject);
    }

    public void RemoveAllEmployeeCardViews()
    {
        foreach (var employeeCardView in _currentEmployeeCardViews)
        {
            Destroy(employeeCardView.GameObject);
        }

        _currentEmployeeCardViews.Clear();
    }

    public void BindButtonEvent(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button != null)
        {
            button.onClick.AddListener(action);
        }
    }

    public void UnbindButtonEvent(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button != null)
        {
            button.onClick.RemoveListener(action);
        }
    }
    
    public void UpdateResultText(float oldSalary, float newSalary)
    {
        _oldSalaryText.text = oldSalary.ToString("C");
        _newSalaryText.text = newSalary.ToString("C");
    }
}

