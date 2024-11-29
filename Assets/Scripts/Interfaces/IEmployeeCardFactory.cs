using UnityEngine;

public interface IEmployeeCardFactory : IFactory<IEmployeeCardPresenter, (EmployeeData, Transform)>
{
}
