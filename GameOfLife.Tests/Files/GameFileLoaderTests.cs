using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.ConsoleApp.Core;
using GameOfLife.ConsoleApp.Files;
using Moq;

namespace FileTests
{
    public class GameFileLoaderTests
    {
        private readonly Mock<IConsole> _console;
        private readonly Mock<IGame> _gameMock;
        private readonly Mock<CommonInputsOutputsDisplay> _inputsOutputDisplayMock;
        private readonly GameFileLoader _gameFileLoader;

        public GameFileLoaderTests()
        {
            _console = new Mock<IConsole>();
            _gameMock = new Mock<IGame>();
            _inputsOutputDisplayMock = new Mock<CommonInputsOutputsDisplay>(_console.Object);
            _gameFileLoader = new GameFileLoader(_inputsOutputDisplayMock.Object, _gameMock.Object);
        }

        [Fact]
        public void LoadGameFromSavedFile_ValidInput_StartsGame()
        {
            var validInput = "3\n6\n#.\n##";

            _gameFileLoader.LoadGameFromSavedFile(validInput);

            _inputsOutputDisplayMock.Verify(d => d.InformInvalidChoice(It.IsAny<string>()), Times.Never);
            _gameMock.Verify(g => g.SetGameField(It.IsAny<GameField>()), Times.Once);
            _gameMock.Verify(g => g.Start(), Times.Once());
        }

        [Fact]
        public void LoadGameFromSavedFile_InvalidInput_ShowsErrorMessage()
        {
            var invalidInput = "shouldNotLoadGame";

            _gameFileLoader.LoadGameFromSavedFile(invalidInput);

            _inputsOutputDisplayMock.Verify(c => c.InformInvalidChoice("Your selected file is not readable! Fix the file or delete it. Press enter to continue."), Times.Once);
            _gameMock.Verify(g => g.SetGameField(It.IsAny<GameField>()), Times.Never);
            _gameMock.Verify(g => g.Start(), Times.Never);
        }
    }
}