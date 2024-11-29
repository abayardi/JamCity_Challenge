using NUnit.Framework;

[TestFixture]
public class EmployeeCardModelTests
{
    private EmployeeCardModel _employeeCardModel;
    private EmployeeData _mockEmployeeData;

    [SetUp]
    public void SetUp()
    {
        _mockEmployeeData = new EmployeeData(1, EmployeeRole.Engineering, SeniorityLevel.Junior);
        _employeeCardModel = new EmployeeCardModel(_mockEmployeeData);
    }

    [Test]
    public void Constructor_ShouldInitializeWithEmployeeData()
    {
        // Assert
        Assert.AreEqual(1, _employeeCardModel.Id);
        Assert.AreEqual(EmployeeRole.Engineering, _employeeCardModel.EmployeeRole);
        Assert.AreEqual(SeniorityLevel.Junior, _employeeCardModel.SeniorityLevel);
    }

    [Test]
    public void ToggleSelection_ShouldToggleIsSelectedAndNotifyChange()
    {
        // Arrange
        bool wasNotified = false;
        _employeeCardModel.OnModelChanged += () => wasNotified = true;

        // Act
        _employeeCardModel.ToggleSelection();

        // Assert
        Assert.IsTrue(_employeeCardModel.IsSelected);
        Assert.IsTrue(wasNotified);

        // Act (toggle de nuevo)
        _employeeCardModel.ToggleSelection();

        // Assert
        Assert.IsFalse(_employeeCardModel.IsSelected);
    }
    [Test]
    public void SetSelected_ShouldChangeIsSelectedAndNotifyChange_WhenDifferent()
    {
        // Arrange
        bool wasNotified = false;
        _employeeCardModel.OnModelChanged += () => wasNotified = true;

        // Act
        _employeeCardModel.SetSelected(true);

        // Assert
        Assert.IsTrue(_employeeCardModel.IsSelected);
        Assert.IsTrue(wasNotified);
    }

    [Test]
    public void SetSelected_ShouldNotNotifyChange_WhenSelectionIsUnchanged()
    {
        // Arrange
        _employeeCardModel.SetSelected(false); // Aseguramos que ya está deseleccionado
        bool wasNotified = false;
        _employeeCardModel.OnModelChanged += () => wasNotified = true;

        // Act
        _employeeCardModel.SetSelected(false);

        // Assert
        Assert.IsFalse(wasNotified);
    }

    [Test]
    public void NotifyChange_ShouldInvokeOnModelChangedEvent()
    {
        // Arrange
        bool wasNotified = false;
        _employeeCardModel.OnModelChanged += () => wasNotified = true;

        // Act
        _employeeCardModel.NotifyChange();

        // Assert
        Assert.IsTrue(wasNotified);
    }


}