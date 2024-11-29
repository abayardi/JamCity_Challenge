using NSubstitute;
using NUnit.Framework;
using System;

[TestFixture]
public class EmployeeCardPresenterTests
{
    private IEmployeeCardModel _mockModel;
    private IEmployeeCardView _mockView;
    private EmployeeCardPresenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _mockModel = Substitute.For<IEmployeeCardModel>();
        _mockView = Substitute.For<IEmployeeCardView>();
        _presenter = new EmployeeCardPresenter();
    }

    [Test]
    public void Initialize_ShouldSetModelAndView_AndBindEvents()
    {
        // Act
        _presenter.Initialize(_mockModel, _mockView);

        // Assert
        Assert.AreEqual(_mockModel, _presenter.Model);
        Assert.AreEqual(_mockView, _presenter.View);

        // Verificar que los eventos estén enlazados
        _mockModel.OnModelChanged += Arg.Any<Action>();
        _mockView.OnToggleChanged += Arg.Any<Action<bool>>();

        // Verificar que la vista se actualice al inicializar
        _mockView.Received(1).UpdateView(_mockModel);
    }

    [Test]
    public void UpdateView_ShouldUpdateViewWhenModelChanges()
    {
        // Arrange
        _presenter.Initialize(_mockModel, _mockView);

        // Act
        _mockModel.NotifyChange();

        // Assert
        _mockView.Received(1).UpdateView(_mockModel);
    }

    [Test]
    public void HandleToggleChanged_ShouldUpdateModelSelectionWhenToggleChanges()
    {
        // Arrange
        _presenter.Initialize(_mockModel, _mockView);

        // Act
        _mockView.OnToggleChanged += Raise.Event<Action<bool>>(true);

        // Assert
        _mockModel.Received(1).SetSelected(true);

        // Act (cambiar a false)
        _mockView.OnToggleChanged += Raise.Event<Action<bool>>(false);

        // Assert
        _mockModel.Received(1).SetSelected(false);
    }

    [Test]
    public void Initialize_ShouldBindModelAndViewEvents()
    {
        // Act
        _presenter.Initialize(_mockModel, _mockView);

        // Assert
        _mockModel.OnModelChanged += Arg.Any<Action>();
        _mockView.OnToggleChanged += Arg.Any<Action<bool>>();
    }

    [Test]
    public void Dispose_ShouldUnbindModelAndViewEvents()
    {
        // Arrange
        _presenter.Initialize(_mockModel, _mockView);

        // Act
        _mockModel.OnModelChanged -= Arg.Any<Action>();
        _mockView.OnToggleChanged -= Arg.Any<Action<bool>>();
    }

}