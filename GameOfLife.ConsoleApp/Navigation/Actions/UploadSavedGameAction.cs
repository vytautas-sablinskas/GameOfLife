using GameOfLife.Data.Constants;
using GameOfLife.ConsoleApp.Files;
using GameOfLife.ConsoleApp.Navigation.Services;

namespace GameOfLife.ConsoleApp.Navigation.Actions
{
    public class UploadSavedGameAction : IMenuAction
    {
        private readonly IGameFileLoader _gameLoader;
        private readonly IObjectSelectionService _fileSelectionService;

        public UploadSavedGameAction(IGameFileLoader gameLoader, IObjectSelectionService fileSelectionService)
        {
            _gameLoader = gameLoader;
            _fileSelectionService = fileSelectionService;
        }

        public void Execute()
        {
            var savedGame = (string)_fileSelectionService.ExecuteService(Paths.SAVED_FILES_FOLDER);
            if (!string.IsNullOrEmpty(savedGame))
            {
                _gameLoader.LoadGameFromSavedFile(savedGame);
            }
        }
    }
}