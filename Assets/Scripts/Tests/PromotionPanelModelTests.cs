using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

[TestFixture]
public class PromotionPanelModelTests
{
    private IEmployeeService _employeeService;
    private IEmployeeCardModel _cardModel;

    [SetUp]
    public void SetUp()
    {
        _employeeService = Substitute.For<IEmployeeService>();
        _cardModel = Substitute.For<IEmployeeCardModel>();
    }
    
    [Test]
    public void AddEmployeeCardModel_AddsCardModelToList()
    {
        // Arrange
        var promotionModel = new PromotionPanelModel(_employeeService, new List<EmployeeData>());

        // Act
        promotionModel.AddEmployeeCardModel(_cardModel);

        // Assert
        Assert.Contains(_cardModel, promotionModel.EmployeeCardModels);
    }

    [Test]
    public void RemoveEmployeeCardModel_RemovesCardModelFromList()
    {
        // Arrange
        var promotionModel = new PromotionPanelModel(_employeeService, new List<EmployeeData>());
        promotionModel.AddEmployeeCardModel(_cardModel);

        // Act
        promotionModel.RemoveEmployeeCardModel(_cardModel);

        // Assert
        Assert.IsEmpty(promotionModel.EmployeeCardModels);
    }

    [Test]
    public void SortEmployeeData_SortsEmployeeCardModels()
    {
        // Arrange
        var promotionModel = new PromotionPanelModel(_employeeService, new List<EmployeeData>());
        var cardModel1 = Substitute.For<IEmployeeCardModel>();
        var cardModel2 = Substitute.For<IEmployeeCardModel>();
        cardModel1.EmployeeRole.Returns(EmployeeRole.Engineering);
        cardModel2.EmployeeRole.Returns(EmployeeRole.Design);
        promotionModel.AddEmployeeCardModel(cardModel2);
        promotionModel.AddEmployeeCardModel(cardModel1);

        // Act
        promotionModel.SortEmployeeData((a, b) => a.EmployeeRole.CompareTo(b.EmployeeRole));

        // Assert
        Assert.AreEqual(cardModel1, promotionModel.EmployeeCardModels[0]);
        Assert.AreEqual(cardModel2, promotionModel.EmployeeCardModels[1]);
    }
}