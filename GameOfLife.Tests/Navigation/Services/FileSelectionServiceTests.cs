using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.ConsoleApp.Services;
using Moq;

namespace NavigationTests
{
    public class FileSelectionServiceTests
    {
        private readonly Mock<IConsole> _consoleMock;
        private readonly Mock<UploadSaveGameDisplay> _mockUploadSaveGameDisplay;
        private readonly Mock<CommonInputsOutputsDisplay> _mockCommonInputsOutputsDisplay;
        private readonly FileSelectionService _service;

        public FileSelectionServiceTests()
        {
            _consoleMock = new Mock<IConsole>();
            _mockUploadSaveGameDisplay = new Mock<UploadSaveGameDisplay>(_consoleMock.Object);
            _mockCommonInputsOutputsDisplay = new Mock<CommonInputsOutputsDisplay>(_consoleMock.Object);
            _service = new FileSelectionService(_mockUploadSaveGameDisplay.Object, _mockCommonInputsOutputsDisplay.Object);
        }

        [Fact]
        public void ExecuteService_NoSavedFiles_InformsUserAndReturnsNull()
        {
            string[] savedFiles = new string[0];
            _mockUploadSaveGameDisplay.Setup(m => m.ListSavedGames(It.IsAny<string>())).Returns(savedFiles);

            var result = _service.ExecuteService("path");

            Assert.Null(result);
            _mockUploadSaveGameDisplay.Verify(m => m.InformAboutNoFilesFound(), Times.Once);
        }

        [Fact]
        public void ExecuteService_UserSelectsValidFile_ReturnsFileContent()
        {
            string[] savedFiles = { "file1.txt", "file2.txt" };
            _mockUploadSaveGameDisplay.Setup(m => m.ListSavedGames(It.IsAny<string>())).Returns(savedFiles);
            _mockCommonInputsOutputsDisplay.Setup(m => m.PromptInput(It.IsAny<string>())).Returns("1");
            File.WriteAllText(savedFiles[0], "content of file1");

            var result = _service.ExecuteService("path");

            Assert.Equal("content of file1", result.ToString());
        }

        [Fact]
        public void ExecuteService_UserSelectsExit_ReturnsNull()
        {
            string[] savedFiles = { "file1.txt", "file2.txt" };
            _mockUploadSaveGameDisplay.Setup(m => m.ListSavedGames(It.IsAny<string>())).Returns(savedFiles);
            _mockCommonInputsOutputsDisplay.Setup(m => m.PromptInput(It.IsAny<string>())).Returns("Q");

            var result = _service.ExecuteService("path");

            Assert.Null(result);
        }

        [Fact]
        public void ExecuteService_UserSelectsInvalidNumber_InformsInvalidChoiceAndPromptsAgain()
        {
            string[] savedFiles = { "file1.txt", "file2.txt" };
            _mockUploadSaveGameDisplay.Setup(m => m.ListSavedGames(It.IsAny<string>())).Returns(savedFiles);
            _mockCommonInputsOutputsDisplay.SetupSequence(m => m.PromptInput(It.IsAny<string>()))
                                          .Returns("3")
                                          .Returns("1");

            File.WriteAllText(savedFiles[0], "content of file1");

            var result = _service.ExecuteService("path");

            Assert.Equal("content of file1", result.ToString());
            _mockCommonInputsOutputsDisplay.Verify(m => m.InformInvalidChoice(""), Times.Once);
        }
    }
}