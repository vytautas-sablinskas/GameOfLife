using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.ConsoleApp.Navigation.Services;
using GameOfLife.Data.Constants;
using Moq;
using Times = Moq.Times;

namespace NavigationTests
{
    public class DimensionSelectionServiceTests
    {
        private readonly Mock<CommonInputsOutputsDisplay> _mockCommonInputsOutputsDisplay;
        private readonly Mock<ChangeBoardSizeDisplay> _mockChangeBoardSizeDisplay;
        private readonly Mock<IConsole> _mockConsole;
        private readonly DimensionSelectionService _service;

        public DimensionSelectionServiceTests()
        {
            _mockConsole = new Mock<IConsole>();
            _mockCommonInputsOutputsDisplay = new Mock<CommonInputsOutputsDisplay>(_mockConsole.Object);
            _mockChangeBoardSizeDisplay = new Mock<ChangeBoardSizeDisplay>(_mockConsole.Object);
            _service = new DimensionSelectionService(_mockCommonInputsOutputsDisplay.Object, _mockChangeBoardSizeDisplay.Object);
        }

        [Fact]
        public void ExecuteService_ValidInput_ReturnsExpectedDimension()
        {
            string inputDimension = "width";
            int expectedDimensionValue = 25;

            _mockCommonInputsOutputsDisplay.Setup(m => m.PromptInput(It.IsAny<string>()))
                                          .Returns(expectedDimensionValue.ToString());

            var result = _service.ExecuteService(inputDimension);

            Assert.Equal(expectedDimensionValue, (int)result);
        }

        [Fact]
        public void ExecuteService_UserExits_ReturnsExitValue()
        {
            string inputDimension = "width";

            _mockCommonInputsOutputsDisplay.Setup(m => m.PromptInput(It.IsAny<string>()))
                                          .Returns("Q");

            var result = _service.ExecuteService(inputDimension);

            Assert.Equal(NavigationActions.EXIT_TO_MAIN_MENU, (int)result);
        }

        [Theory]
        [InlineData("invalidNumber", "25")]
        [InlineData("", "10")]
        public void ExecuteService_InvalidInput_PromptsAgain(string invalidNumber, string validNumber)
        {
            string inputDimension = "width";

            _mockCommonInputsOutputsDisplay.SetupSequence(m => m.PromptInput(It.IsAny<string>()))
                                          .Returns(invalidNumber)
                                          .Returns(validNumber);

            var result = _service.ExecuteService(inputDimension);

            Assert.Equal(int.Parse(validNumber), (int)result);
            _mockChangeBoardSizeDisplay.Verify(m => m.InformInvalidNumber(invalidNumber), Times.Once);
        }
    }
}