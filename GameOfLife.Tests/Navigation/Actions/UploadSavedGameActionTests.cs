using GameOfLife.ConsoleApp.Files;
using GameOfLife.ConsoleApp.Navigation.Actions;
using GameOfLife.ConsoleApp.Navigation.Services;
using Moq;

namespace NavigationTests
{
    public class UploadSavedGameActionTests
    {
        private readonly Mock<IGameFileLoader> _gameLoader;
        private readonly Mock<IObjectSelectionService> _fileSelectionService;
        private readonly UploadSavedGameAction _uploadSavedGameAction;

        public UploadSavedGameActionTests()
        {
            _gameLoader = new Mock<IGameFileLoader>();
            _fileSelectionService = new Mock<IObjectSelectionService>();
            _uploadSavedGameAction = new UploadSavedGameAction(_gameLoader.Object, _fileSelectionService.Object);
        }

        [Fact]
        public void Execute_ValidInput_ShouldLoadGame()
        {
            var gameMap = "Should load game";
            _fileSelectionService.Setup(g => g.ExecuteService(It.IsAny<string>())).Returns(gameMap);
            _gameLoader.Setup(g => g.LoadGameFromSavedFile(gameMap));

            _uploadSavedGameAction.Execute();

            _gameLoader.Verify(g => g.LoadGameFromSavedFile(gameMap), Times.Once);
        }

        [Fact]
        public void Execute_InvalidInput_ShouldNotLoadGame()
        {
            var gameMap = "";
            _fileSelectionService.Setup(g => g.ExecuteService(It.IsAny<string>())).Returns(gameMap);
            _gameLoader.Setup(g => g.LoadGameFromSavedFile(gameMap));

            _uploadSavedGameAction.Execute();

            _gameLoader.Verify(g => g.LoadGameFromSavedFile(gameMap), Times.Never);
        }
    }
}