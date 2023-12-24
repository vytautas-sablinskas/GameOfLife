using Moq;
using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.Data.Entities;
using GameOfLife.ConsoleApp.Navigation.Services;
using GameOfLife.Data.Constants;
using Times = Moq.Times;

namespace NavigationTests
{
    public class ChangeGameFieldDimensionsActionTests
    {
        private readonly Mock<GameFieldDimensions> _mockGameFieldDimensions;
        private readonly Mock<IObjectSelectionService> _mockDimensionSelectionService;
        private readonly Mock<IConsole> _consoleMock;
        private readonly Mock<ChangeBoardSizeDisplay> _mockChangeBoardSizeDisplay;
        private readonly ChangeGameFieldDimensionsAction _action;

        public ChangeGameFieldDimensionsActionTests()
        {
            _mockGameFieldDimensions = new Mock<GameFieldDimensions>();
            _mockDimensionSelectionService = new Mock<IObjectSelectionService>();
            _consoleMock = new Mock<IConsole>();
            _mockChangeBoardSizeDisplay = new Mock<ChangeBoardSizeDisplay>(_consoleMock.Object);

            _action = new ChangeGameFieldDimensionsAction(_mockGameFieldDimensions.Object, _mockDimensionSelectionService.Object, _mockChangeBoardSizeDisplay.Object);
        }

        [Fact]
        public void Execute_ValidDimensions_ChangesDimensionsAndDisplaysMessage()
        {
            var width = 10;
            var height = 20;
            _mockDimensionSelectionService.Setup(s => s.ExecuteService(Words.WIDTH.ToLower())).Returns(width);
            _mockDimensionSelectionService.Setup(s => s.ExecuteService(Words.HEIGHT.ToLower())).Returns(height);
            _mockGameFieldDimensions.SetupAllProperties();

            _action.Execute();

            _mockGameFieldDimensions.VerifySet(m => m.Width = 10);
            _mockGameFieldDimensions.VerifySet(m => m.Height = 20);
            _mockChangeBoardSizeDisplay.Verify(d => d.InformAboutSuccessfullyChangedValues(width, height), Times.Once);
        }

        [Fact]
        public void Execute_ExitBeforeWidth_DoesNotChangeDimensionsOrDisplayMessage()
        {
            _mockDimensionSelectionService.Setup(s => s.ExecuteService(It.IsAny<string>())).Returns(-1);

            _action.Execute();

            _mockChangeBoardSizeDisplay.Verify(d => d.InformAboutSuccessfullyChangedValues(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Execute_ExitBeforeHeight_DoesNotChangeDimensionsOrDisplayMessage()
        {
            var width = 10;
            var height = -1;
            _mockDimensionSelectionService.Setup(s => s.ExecuteService(Words.WIDTH.ToLower())).Returns(width);
            _mockDimensionSelectionService.Setup(s => s.ExecuteService(Words.HEIGHT.ToLower())).Returns(height);

            _action.Execute();

            _mockGameFieldDimensions.VerifySet(m => m.Width = width, Times.Never);
            _mockGameFieldDimensions.VerifySet(m => m.Height = height, Times.Never);
            _mockChangeBoardSizeDisplay.Verify(d => d.InformAboutSuccessfullyChangedValues(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
    }
}