using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.ConsoleApp.Files;
using Moq;

namespace FileTests
{
    public class GameFileSaverTests
    {
        private readonly Mock<IConsole> _consoleMock;
        private readonly Mock<FileManager> _fileManagerMock;
        private readonly Mock<UploadSaveGameDisplay> _uploadSaveGameDisplayMock;
        private readonly GameFileSaver _gameFileSaver;

        private const string MockedFilePath = "mockedFilePath.txt";

        public GameFileSaverTests()
        {
            _consoleMock = new Mock<IConsole>();
            _fileManagerMock = new Mock<FileManager>();
            _uploadSaveGameDisplayMock = new Mock<UploadSaveGameDisplay>(_consoleMock.Object);

            _uploadSaveGameDisplayMock.Setup(u => u.GetSessionFileName()).Returns(MockedFilePath);

            _gameFileSaver = new GameFileSaver(_fileManagerMock.Object, _uploadSaveGameDisplayMock.Object);
        }

        [Fact]
        public void SaveGame_CallsSaveToFileWithCorrectArguments()
        {
            var mockedGameFieldMap = "mockedGameFieldMap";

            _gameFileSaver.SaveGame(mockedGameFieldMap);

            _fileManagerMock.Verify(f => f.SaveToFile(MockedFilePath, mockedGameFieldMap), Times.Once);
        }

        [Fact]
        public void SaveGame_ReturnsCorrectFilePath()
        {
            var result = _gameFileSaver.SaveGame("mockedGameFieldMap");

            Assert.Equal(MockedFilePath, result);
        }
    }
}