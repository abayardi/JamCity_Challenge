using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class EmployeeRepository : IEmployeeRepository
{
    private List<EmployeeData> _employees;
    private string _fileName;

    public EmployeeRepository(string fileName)
    {
        _fileName = fileName;
        _employees = new List<EmployeeData>();
    }

    public List<EmployeeData> GetAll()
    {
        if (_employees.Count == 0)
        {
            _employees = LoadEmployeesFromCSV(_fileName);
        }

        return _employees;
    }

    public EmployeeData GetById(int id)
    {
        return _employees.FirstOrDefault(e => e.Id == id);
    }

    public void Add(EmployeeData employee)
    {
        _employees.Add(employee);
    }

    public void Update(EmployeeData employee)
    {
        var existing = GetById(employee.Id);
        if (existing != null)
        {
            existing = employee;
        }
    }

    public void Delete(int id)
    {
        var employee = GetById(id);
        if (employee != null)
        {
            _employees.Remove(employee);
        }
    }

    public List<EmployeeData> LoadEmployeesFromCSV(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);
        if (csvFile == null)
        {
            throw new FileNotFoundException($"The file {fileName} was not found in the Resources folder.");
        }

        List<EmployeeData> employees = new List<EmployeeData>();
        string[] lines = csvFile.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(',');

            if (fields.Length < 3)
            {
                Debug.LogWarning($"Invalid line in CSV: {line}");
                continue;
            }

            if (!int.TryParse(fields[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int id))
            {
                Debug.LogWarning($"Failed to parse ID: {fields[0]}");
                continue;
            }

            if (!Enum.TryParse(fields[1], true, out EmployeeRole role))
            {
                Debug.LogWarning($"Failed to parse Role: {fields[1]}");
                continue;
            }

            if (!Enum.TryParse(fields[2], true, out SeniorityLevel seniority))
            {
                Debug.LogWarning($"Failed to parse Seniority: {fields[2]}");
                continue;
            }

            EmployeeData data = new EmployeeData(id, role, seniority);
            employees.Add(data);
        }

        return employees;
    }
}
