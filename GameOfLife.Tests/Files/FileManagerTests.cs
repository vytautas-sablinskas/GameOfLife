using GameOfLife.ConsoleApp.Files;

namespace FileTests
{
    public class FileManagerTests
    {
        private readonly string _tempFile;
        private readonly FileManager _fileManager;

        public FileManagerTests()
        {
            _tempFile = Path.GetTempFileName();
            _fileManager = new FileManager();
        }

        [Fact]
        public void ReadFileToString_ValidInput_ReturnsCorrectLines()
        {
            File.WriteAllLines(_tempFile, new string[] { "Line1", "Line2" });

            var lines = _fileManager.ReadFileToString(_tempFile);

            Assert.Equal(new string[] { "Line1", "Line2" }, lines);
        }

        [Fact]
        public void SaveToFile_ValidInput_SavesCorrectContent()
        {
            _fileManager.SaveToFile(_tempFile, "TestContent");
            var content = File.ReadAllText(_tempFile);

            Assert.Equal("TestContent", content);
        }

        [Fact]
        public void CreateSavedFilesFolderIfNotExisting__FolderNotExisting_CreatesFolder()
        {
            var tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            _fileManager.CreateSavedFilesFolderIfNotExisting(tempFolder);

            Assert.True(Directory.Exists(tempFolder));
        }
    }
}