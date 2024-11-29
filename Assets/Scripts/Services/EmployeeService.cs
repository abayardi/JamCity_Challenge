using System.Collections.Generic;

public class EmployeeService : IEmployeeService
{
    private bool _isInitialized;
    public bool IsInitialized => _isInitialized;

    private EmployeeSalaryConfig _config;

    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository, EmployeeSalaryConfig config)
    {
        _config = config;
        _repository = repository;
    }

    public void Initialize()
    {
        _isInitialized = true;
    }

    public void Shutdown()
    {
        _isInitialized = false;
    }

    public void SetEmployeeSalaryConfig(EmployeeSalaryConfig config)
    {
        _config = config;
    }

    public float CalculateCurrentSalary(IEmployeeCardModel model)
    {
        if (_config == null )
        {
            return 0;
        }

        SalaryConfig salaryConfig = _config.GetSalaryConfiguration(model.EmployeeRole, model.SeniorityLevel);

        if (salaryConfig == null)
        {
            return 0;
        }

        return salaryConfig.BaseSalary;
    }

    public float CalculateSalaryIncrement(IEmployeeCardModel model)
    {
        if (_config == null)
        {
            return 0;
        }

        SalaryConfig salaryConfig = _config.GetSalaryConfiguration(model.EmployeeRole, model.SeniorityLevel);

        if (salaryConfig == null)
        {
            return 0;
        }

        return salaryConfig.BaseSalary * (1f + salaryConfig.IncrementPercentage);
    }

    public List<EmployeeData> GetAllEmployees()
    {
        return _repository.GetAll();
    }

    public EmployeeData GetEmployeeById(int id)
    {
        return _repository.GetById(id);
    }

    public void AddEmployee(EmployeeData employee)
    {
        _repository.Add(employee);
    }

    public void UpdateEmployee(EmployeeData employee)
    {
        _repository.Update(employee);
    }

    public void DeleteEmployee(int id)
    {
        _repository.Delete(id);
    }

}
