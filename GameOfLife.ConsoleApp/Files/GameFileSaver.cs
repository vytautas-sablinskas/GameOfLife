using GameOfLife.ConsoleApp.ConsoleManagers;

namespace GameOfLife.ConsoleApp.Files
{
    public class GameFileSaver : IGameFileSaver
    {
        private readonly string _currentSessionFilePath;
        private readonly FileManager _fileManager;
        private readonly UploadSaveGameDisplay _uploadSaveGameDisplay;

        public GameFileSaver(FileManager fileManager, UploadSaveGameDisplay uploadSaveGameDisplay)
        {
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(_fileManager));
            _uploadSaveGameDisplay = uploadSaveGameDisplay;

            _currentSessionFilePath = _uploadSaveGameDisplay.GetSessionFileName();
        }

        public string SaveGame(string gameFieldMap)
        {
            _fileManager.SaveToFile(_currentSessionFilePath, gameFieldMap);

            return _currentSessionFilePath;
        }
    }
}