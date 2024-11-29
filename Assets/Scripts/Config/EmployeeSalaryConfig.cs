using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SalaryConfig
{
    public SeniorityLevel Seniority;
    public float BaseSalary;
    public float IncrementPercentage;

    public SalaryConfig(SeniorityLevel seniorityLevel, float baseSalary, float incrementPercentage)
    {
        Seniority = seniorityLevel;
        BaseSalary = baseSalary;
        IncrementPercentage = incrementPercentage;
    }
}

[Serializable]
public class RoleConfig
{
    public EmployeeRole Role;
    public List<SalaryConfig> SalaryConfigs;

    public RoleConfig(EmployeeRole role, List<SalaryConfig> salaryConfigs)
    {
        Role = role;
        SalaryConfigs = salaryConfigs;
    }
}

[CreateAssetMenu(fileName = "EmployeeSalaryConfig", menuName = "Scriptable Objects/Employee Salary Config")]
public class EmployeeSalaryConfig : ScriptableObject
{
    public List<RoleConfig> RoleConfigs;

    public SalaryConfig GetSalaryConfiguration(EmployeeRole role, SeniorityLevel seniority)
    {
        foreach (var roleConfig in RoleConfigs)
        {
            if (roleConfig.Role == role)
            {
                foreach (var salaryConfig in roleConfig.SalaryConfigs)
                {
                    if (salaryConfig.Seniority == seniority)
                    {
                        return salaryConfig;
                    }
                }
            }
        }

        Debug.LogWarning($"No salary configuration found for Role: {role}, Seniority: {seniority}. Returning null.");
        return null;
    }
}