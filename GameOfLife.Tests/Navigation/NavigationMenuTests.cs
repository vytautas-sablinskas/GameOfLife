using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.ConsoleApp.Navigation.Actions;
using GameOfLife.Data.Entities;
using GameOfLife.Tests;
using Moq;

namespace NavigationTests
{
    public class NavigationMenuTests
    {
        private readonly Mock<IConsole> _consoleMock;
        private readonly Mock<IMenuActionFactory> _menuActionFactoryMock;
        private readonly Mock<NavigationDisplay> _mockNavigationDisplay;
        private readonly Mock<CommonInputsOutputsDisplay> _mockCommonInputsOutputsDisplay;
        private readonly NavigationMenu _navigationMenu;
        private const int _maxRuns = 1;

        public NavigationMenuTests()
        {
            var testRunConditon = new TestRunCondition(_maxRuns);
            _menuActionFactoryMock = new Mock<IMenuActionFactory>();
            _consoleMock = new Mock<IConsole>();
            _mockNavigationDisplay = new Mock<NavigationDisplay>();
            _mockCommonInputsOutputsDisplay = new Mock<CommonInputsOutputsDisplay>(_consoleMock.Object);
            _navigationMenu = new NavigationMenu(_menuActionFactoryMock.Object,
                                                 _mockNavigationDisplay.Object,
                                                 _mockCommonInputsOutputsDisplay.Object,
                                                 testRunConditon);
        }

        [Fact]
        public void ShowMenu_InvalidChoice_InformsUserOfInvalidChoice()
        {
            _mockCommonInputsOutputsDisplay.SetupSequence(m => m.PromptInput(It.IsAny<string>()))
                                          .Returns("invalid_choice")
                                          .Returns("exit");

            _mockNavigationDisplay.Setup(m => m.InvalidChoiceError()).Returns("Invalid choice!");

            _navigationMenu.ShowMenu();

            _mockCommonInputsOutputsDisplay.Verify(m => m.PromptInput("Invalid choice!"), Times.Once);
        }

        [Fact]
        public void ShowMenu_ValidChoice_ExecutesCorrespondingAction()
        {
            var mockAction = new Mock<IMenuAction>();
            _menuActionFactoryMock.Setup(m => m.CreateAction("valid_choice", It.IsAny<GameFieldDimensions>())).Returns(mockAction.Object);
            _mockCommonInputsOutputsDisplay.Setup(m => m.PromptInput(It.IsAny<string>())).Returns("valid_choice");

            _navigationMenu.ShowMenu();

            mockAction.Verify(a => a.Execute(), Times.Once);
        }
    }
}