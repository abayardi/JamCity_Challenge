using UnityEngine;

public enum EmployeeRole
{
    None = 0,
    HR = 1,
    Engineering = 2,
    Artist = 3,
    Design = 4,
    PM = 5,
    CEO = 6
}

public enum SeniorityLevel
{
    None = 0,
    Junior = 1,
    SemiSenior = 2,
    Senior = 3
}

public class EmployeeData
{
    public int Id { get; private set; }
    public EmployeeRole EmployeeRole { get; private set; }
    public SeniorityLevel SeniorityLevel { get; private set; }

    public EmployeeData(int id, EmployeeRole employeeRole, SeniorityLevel seniorityLevel)
    {
        Id = id;
        EmployeeRole = employeeRole;
        SeniorityLevel = seniorityLevel;
    }

    public void UpdateData(int id, EmployeeRole employeeRole, SeniorityLevel seniorityLevel)
    {
        Id = id;
        EmployeeRole = employeeRole;
        SeniorityLevel = seniorityLevel;
    }
}