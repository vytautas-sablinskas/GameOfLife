using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.ConsoleApp.Navigation.Services;
using GameOfLife.Data.Constants;

namespace GameOfLife.ConsoleApp.Services
{
    public class FileSelectionService : IObjectSelectionService
    {
        private readonly UploadSaveGameDisplay _uploadSaveGameDisplay;
        private readonly CommonInputsOutputsDisplay _commonInputsOutputsDisplay;

        public FileSelectionService(UploadSaveGameDisplay uploadSaveGameDisplay, CommonInputsOutputsDisplay commonInputsOutputsDisplay)
        {
            _uploadSaveGameDisplay = uploadSaveGameDisplay;
            _commonInputsOutputsDisplay = commonInputsOutputsDisplay;
        }

        public object ExecuteService(object input)
        {
            var folderPath = (string)input;

            return SelectAndReadSavedGame(folderPath);
        }

        private string SelectAndReadSavedGame(string folderPath)
        {
            _commonInputsOutputsDisplay.ClearMessages();

            var savedFiles = _uploadSaveGameDisplay.ListSavedGames(folderPath);
            if (!savedFiles.Any())
            {
                _uploadSaveGameDisplay.InformAboutNoFilesFound();
                return null;
            }

            int chosenNumber = GetUserChoiceFromDisplayedFiles(savedFiles.Length, savedFiles);
            if (chosenNumber == NavigationActions.EXIT_TO_MAIN_MENU)
                return null;

            return File.ReadAllText(savedFiles[chosenNumber - 1]);
        }

        private int GetUserChoiceFromDisplayedFiles(int maxChoice, string[] savedFiles)
        {
            while (true)
            {
                _commonInputsOutputsDisplay.ClearMessages();
                _uploadSaveGameDisplay.DisplaySavedGames(savedFiles);

                var input = _commonInputsOutputsDisplay.PromptInput("Enter the number of your choice: ");
                if (_commonInputsOutputsDisplay.UserSelectedExitToMainMenu(input))
                {
                    return NavigationActions.EXIT_TO_MAIN_MENU;
                }

                var isValidNumber = int.TryParse(input, out var chosenNumber) && chosenNumber > 0 && chosenNumber <= maxChoice;
                if (!isValidNumber)
                {
                    _commonInputsOutputsDisplay.InformInvalidChoice();
                    continue;
                }

                return chosenNumber;
            }
        }
    }
}