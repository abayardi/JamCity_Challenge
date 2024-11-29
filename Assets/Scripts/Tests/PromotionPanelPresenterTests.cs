using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

[TestFixture]
public class PromotionPanelPresenterTests
{
    private IEmployeeService _employeeService;
    private IEmployeeCardFactory _employeeCardFactory;
    private IPromotionPanelModel _promotionPanelModel;
    private IPromotionPanelView _promotionPanelView;
    private IPromotionPanelPresenter _promotionPanelPresenter;

    [SetUp]
    public void SetUp()
    {
        _employeeService = Substitute.For<IEmployeeService>();
        _promotionPanelModel = Substitute.For<IPromotionPanelModel>();
        _promotionPanelView = Substitute.For<IPromotionPanelView>();
        _employeeCardFactory = Substitute.For<IEmployeeCardFactory>();
        _promotionPanelPresenter = new PromotionPanelPresenter(_employeeCardFactory);
    }

    [Test]
    public void Initialize_ModelsAndView_AreSetCorrectly()
    {
        // Arrange
        _promotionPanelModel.EmployeeDataList.Returns(new List<EmployeeData>());

        // Act
        _promotionPanelPresenter.Initialize(_promotionPanelModel, _promotionPanelView);

        // Assert
        Assert.AreEqual(_promotionPanelModel, _promotionPanelPresenter.Model);
        Assert.AreEqual(_promotionPanelView, _promotionPanelPresenter.View);
    }

    [Test]
    public void Initialize_BindsViewMethods()
    {
        // Arrange
        _promotionPanelModel.EmployeeDataList.Returns(new List<EmployeeData>());

        // Act
        _promotionPanelPresenter.Initialize(_promotionPanelModel, _promotionPanelView);

        // Assert
        _promotionPanelView.Received().BindButtonEvent(Arg.Any<Button>(), Arg.Any<UnityAction>());
    }

    [Test]
    public void InitializeEmployeeCards_CreatesAndAddsEmployeeCards()
    {
        // Arrange
        List<EmployeeData> employeeDataList = new List<EmployeeData>
        {
            new EmployeeData(1, EmployeeRole.Engineering, SeniorityLevel.Junior),
            new EmployeeData(2, EmployeeRole.Design, SeniorityLevel.Senior),
            new EmployeeData(3, EmployeeRole.PM, SeniorityLevel.Junior),
            new EmployeeData(4, EmployeeRole.HR, SeniorityLevel.SemiSenior),
            new EmployeeData(5, EmployeeRole.CEO, SeniorityLevel.None)
        };

        foreach (var data in employeeDataList)
        {
            var mockCardPresenter = Substitute.For<IEmployeeCardPresenter>();
            mockCardPresenter.View.Returns(Substitute.For<IEmployeeCardView>());
            mockCardPresenter.Model.Returns(Substitute.For<IEmployeeCardModel>());

            _employeeCardFactory.Create((data, _promotionPanelView.Transform)).Returns(mockCardPresenter);
        }

        _promotionPanelModel.EmployeeDataList.Returns(employeeDataList);

        // Act
        _promotionPanelPresenter.Initialize(_promotionPanelModel, _promotionPanelView);

        // Assert
        _promotionPanelView.Received(employeeDataList.Count).AddEmployeeCardView(Arg.Any<IEmployeeCardView>());
        _promotionPanelModel.Received(employeeDataList.Count).AddEmployeeCardModel(Arg.Any<IEmployeeCardModel>());
    }

    [Test]
    public void Initialize_HandlesEmptyEmployeeList()
    {
        // Arrange
        var emptyEmployeeList = new List<EmployeeData>();
        _promotionPanelModel.EmployeeDataList.Returns(emptyEmployeeList);

        // Act
        _promotionPanelPresenter.Initialize(_promotionPanelModel, _promotionPanelView);

        // Assert
        _promotionPanelView.Received(0).AddEmployeeCardView(Arg.Any<IEmployeeCardView>());
        _promotionPanelModel.Received(0).AddEmployeeCardModel(Arg.Any<IEmployeeCardModel>());
    }

    [Test]
    public void Initialize_CreatesEmployeeCardsForAllEmployeeTypes()
    {
        // Arrange
        var employeeDataList = new List<EmployeeData>
        {
            new EmployeeData(1, EmployeeRole.Engineering, SeniorityLevel.Junior),
            new EmployeeData(2, EmployeeRole.Design, SeniorityLevel.Senior),
            new EmployeeData(3, EmployeeRole.PM, SeniorityLevel.SemiSenior)
        };

        foreach (var data in employeeDataList)
        {
            var mockCardPresenter = Substitute.For<IEmployeeCardPresenter>();
            mockCardPresenter.View.Returns(Substitute.For<IEmployeeCardView>());
            mockCardPresenter.Model.Returns(Substitute.For<IEmployeeCardModel>());

            _employeeCardFactory.Create((data, _promotionPanelView.Transform)).Returns(mockCardPresenter);
        }

        _promotionPanelModel.EmployeeDataList.Returns(employeeDataList);

        // Act
        _promotionPanelPresenter.Initialize(_promotionPanelModel, _promotionPanelView);

        // Assert
        _promotionPanelView.Received(employeeDataList.Count).AddEmployeeCardView(Arg.Any<IEmployeeCardView>());
        _promotionPanelModel.Received(employeeDataList.Count).AddEmployeeCardModel(Arg.Any<IEmployeeCardModel>());
    }

    [Test]
    public void Dispose_RemovesEmployeeCardsAndNullifiesModelAndView()
    {
        // Arrange
        List<EmployeeData> employeeDataList = new List<EmployeeData>
        {
            new EmployeeData(1, EmployeeRole.Engineering, SeniorityLevel.Junior),
            new EmployeeData(2, EmployeeRole.Design, SeniorityLevel.Senior),
            new EmployeeData(3, EmployeeRole.PM, SeniorityLevel.Junior),
            new EmployeeData(4, EmployeeRole.HR, SeniorityLevel.SemiSenior),
            new EmployeeData(5, EmployeeRole.CEO, SeniorityLevel.None)
        };

        _promotionPanelModel.EmployeeDataList.Returns(employeeDataList);
        _promotionPanelPresenter.Initialize(_promotionPanelModel, _promotionPanelView);

        // Act
        _promotionPanelPresenter.Dispose();

        // Assert
        _promotionPanelView.Received().RemoveAllEmployeeCardViews();
        Assert.IsNull(_promotionPanelPresenter.Model);
        Assert.IsNull(_promotionPanelPresenter.View);
    }

    [TestCase(0f, 0f)]
    [TestCase(1000f, 0.2f)]
    public void CalculateSalaries_ShouldUpdateViewWithCorrectSalaryValues(float baseSalary, float salaryIncrement)
    {
        // Arrange
        IEmployeeCardModel cardModel = Substitute.For<IEmployeeCardModel>();
        List<IEmployeeCardModel> employeeCardModels = new List<IEmployeeCardModel>
        {
            cardModel
        };

        EmployeeSalaryConfig salaryConfig = ScriptableObject.CreateInstance<EmployeeSalaryConfig>();
        salaryConfig.RoleConfigs = new List<RoleConfig>
        {
            new RoleConfig(EmployeeRole.Engineering, new List<SalaryConfig>
            {
                new SalaryConfig(SeniorityLevel.Junior, baseSalary, salaryIncrement)
            })
        };

        List<EmployeeData> employeeDataList = new List<EmployeeData>
        {
            new EmployeeData(1, EmployeeRole.Engineering, SeniorityLevel.Junior)
        };

        _employeeService.SetEmployeeSalaryConfig(salaryConfig);
        _employeeService.CalculateCurrentSalary(cardModel).Returns(baseSalary);
        _employeeService.CalculateSalaryIncrement(cardModel).Returns(baseSalary * (1 + salaryIncrement));
        _promotionPanelModel.EmployeeDataList.Returns(employeeDataList);
        _promotionPanelModel.EmployeeService.Returns(_employeeService);
        _promotionPanelPresenter.Initialize(_promotionPanelModel, _promotionPanelView);

        // Act
        _promotionPanelPresenter.CalculateSalaries(employeeCardModels);

        // Assert
        _promotionPanelView.Received(1).UpdateResultText(baseSalary, (baseSalary * (1 + salaryIncrement)));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void UpdateAllEmployeeSelections_ShouldUpdateSelectionStateAndNotifyChange(bool testValue)
    {
        // Arrange
        List<EmployeeData> employeeDataList = new List<EmployeeData>();

        var mockEmployeeCardModels = new List<IEmployeeCardModel>
        {
            Substitute.For<IEmployeeCardModel>(),
            Substitute.For<IEmployeeCardModel>()
        };

        foreach (var card in mockEmployeeCardModels)
        {
            card.IsSelected.Returns(!testValue);
        }

        _promotionPanelModel.EmployeeCardModels.Returns(mockEmployeeCardModels);
        _promotionPanelModel.EmployeeDataList.Returns(employeeDataList);
        _promotionPanelPresenter.Initialize(_promotionPanelModel, _promotionPanelView);

        // Act
        _promotionPanelPresenter.UpdateAllEmployeeSelections(testValue);

        // Assert
        foreach (var card in mockEmployeeCardModels)
        {
            card.Received(1).SetSelected(testValue);
        }

        _promotionPanelModel.Received().NotifyChange();
    }
}
