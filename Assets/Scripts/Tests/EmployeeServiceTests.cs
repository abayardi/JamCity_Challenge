using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class EmployeeServiceTests
{
    private IEmployeeRepository _mockRepository;
    private EmployeeSalaryConfig _salaryConfig;
    private EmployeeService _employeeService;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = Substitute.For<IEmployeeRepository>();
        _salaryConfig = ScriptableObject.CreateInstance<EmployeeSalaryConfig>();
        _employeeService = new EmployeeService(_mockRepository, _salaryConfig);
    }

    [Test]
    public void Initialize_IsInitialized_True()
    {
        // Act
        _employeeService.Initialize();
        // Assert
        Assert.IsTrue(_employeeService.IsInitialized);
    }

    [Test]
    public void Shutdown_IsInitialized_False()
    {
        // Arrange
        _employeeService.Initialize();
        // Act
        _employeeService.Shutdown();
        // Assert
        Assert.IsFalse(_employeeService.IsInitialized);
    }

    [TestCase(EmployeeRole.Engineering, SeniorityLevel.Junior, 3000f, 0f)]
    public void CalculateBaseSalary_ShouldReturnCorrectSalary_WhenConfigurationExists(EmployeeRole employeeRole, SeniorityLevel seniorityLevel, float baseSalary, float increment)
    {
        // Arrange
        var mockCardModel = Substitute.For<IEmployeeCardModel>();
        mockCardModel.EmployeeRole.Returns(employeeRole);
        mockCardModel.SeniorityLevel.Returns(seniorityLevel);
        
        var salaryConfig = new SalaryConfig(seniorityLevel, baseSalary, increment);
        
        var employeeSalaryConfig = ScriptableObject.CreateInstance<EmployeeSalaryConfig>();
        employeeSalaryConfig.RoleConfigs = new List<RoleConfig>
        {
            new RoleConfig(employeeRole, new List<SalaryConfig> { salaryConfig })
        };

        _employeeService.SetEmployeeSalaryConfig(employeeSalaryConfig);

        // Act
        var result = _employeeService.CalculateCurrentSalary(mockCardModel);

        // Assert
        Assert.AreEqual(baseSalary, result);
    }

    [Test]
    public void CalculateCurrentSalary_ShouldReturnZero_WhenConfigurationDoesNotExist()
    {
        // Arrange
        var mockCardModel = Substitute.For<IEmployeeCardModel>();
        mockCardModel.EmployeeRole.Returns(EmployeeRole.Engineering);
        mockCardModel.SeniorityLevel.Returns(SeniorityLevel.Junior);
        _employeeService.SetEmployeeSalaryConfig(null);

        // Act
        var result = _employeeService.CalculateCurrentSalary(mockCardModel);

        // Assert
        Assert.AreEqual(0f, result);
    }

    [TestCase(EmployeeRole.Engineering, SeniorityLevel.Junior, 3000f, 0.1f)]
    [TestCase(EmployeeRole.CEO, SeniorityLevel.Senior, 1000f, 0.5f)]
    [TestCase(EmployeeRole.Design, SeniorityLevel.SemiSenior, 2000f, 0f)]
    public void CalculateSalaryIncrement_ShouldReturnCorrectIncrement_WhenConfigurationExists(EmployeeRole employeeRole, SeniorityLevel seniorityLevel, float baseSalary, float increment)
    {
        // Arrange
        var mockCardModel = Substitute.For<IEmployeeCardModel>();
        mockCardModel.EmployeeRole.Returns(employeeRole);
        mockCardModel.SeniorityLevel.Returns(seniorityLevel);

        var salaryConfig = new SalaryConfig(seniorityLevel, baseSalary, increment);

        var employeeSalaryConfig = ScriptableObject.CreateInstance<EmployeeSalaryConfig>();
        employeeSalaryConfig.RoleConfigs = new List<RoleConfig>
        {
            new RoleConfig(employeeRole, new List<SalaryConfig> { salaryConfig })
        };

        _employeeService.SetEmployeeSalaryConfig(employeeSalaryConfig);

        // Act
        var result = _employeeService.CalculateSalaryIncrement(mockCardModel);

        // Assert
        Assert.AreEqual((baseSalary * (1 + increment)), result);
    }

    [Test]
    public void CalculateSalaryIncrement_ShouldReturnZero_WhenConfigurationDoesNotExist()
    {
        // Arrange
        var mockCardModel = Substitute.For<IEmployeeCardModel>();
        mockCardModel.EmployeeRole.Returns(EmployeeRole.Design);
        mockCardModel.SeniorityLevel.Returns(SeniorityLevel.Junior);

        _employeeService.SetEmployeeSalaryConfig(null);

        // Act
        var result = _employeeService.CalculateSalaryIncrement(mockCardModel);

        // Assert
        Assert.AreEqual(0f, result);
    }

    [Test]
    public void GetAllEmployees_ShouldReturnListFromRepository()
    {
        // Arrange
        var mockEmployees = new List<EmployeeData>
        {
            new EmployeeData(1, EmployeeRole.Engineering, SeniorityLevel.Junior),
            new EmployeeData(2, EmployeeRole.Design, SeniorityLevel.Senior)
        };
        _mockRepository.GetAll().Returns(mockEmployees);

        // Act
        var result = _employeeService.GetAllEmployees();

        // Assert
        Assert.AreEqual(mockEmployees, result);
    }

}
