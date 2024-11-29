using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeCardView : MonoBehaviour, IEmployeeCardView
{
    public event Action<bool> OnToggleChanged;
    public Transform Transform => transform;
    public GameObject GameObject => gameObject;

    [SerializeField]
    private TMP_Text _idText;

    [SerializeField]
    private TMP_Text _seniorityText;

    [SerializeField]
    private TMP_Text _roleText;

    [SerializeField]
    private Toggle _toggle;

    public void UpdateView(IEmployeeCardModel model)
    {
        _idText.text = model.Id.ToString();
        _roleText.text = model.EmployeeRole.ToString();
        _seniorityText.text = model.SeniorityLevel.ToString();

        _toggle.onValueChanged.RemoveAllListeners();
        _toggle.isOn = model.IsSelected;
        _toggle.onValueChanged.AddListener(value => OnToggleChanged?.Invoke(value));
    }

}
