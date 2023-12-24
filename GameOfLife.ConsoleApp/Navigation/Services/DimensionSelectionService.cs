using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.Data.Constants;

namespace GameOfLife.ConsoleApp.Navigation.Services
{
    public class DimensionSelectionService : IObjectSelectionService
    {
        private readonly CommonInputsOutputsDisplay _commonInputsOutputsDisplay;
        private readonly ChangeBoardSizeDisplay _changeBoardSizeDisplay;

        public DimensionSelectionService(CommonInputsOutputsDisplay commonInputsOutputsDisplay, ChangeBoardSizeDisplay changeBoardSizeDisplay)
        {
            _commonInputsOutputsDisplay = commonInputsOutputsDisplay;
            _changeBoardSizeDisplay = changeBoardSizeDisplay;
        }

        public object ExecuteService(object input)
        {
            var dimensionName = (string)input;
            return (int)GetDimension(dimensionName);
        }

        private int? GetDimension(string dimensionName)
        {
            _commonInputsOutputsDisplay.ClearMessages();

            while (true)
            {
                var input = _commonInputsOutputsDisplay.PromptInput(
                    messageToUser: $"Select {dimensionName} with a number between 1-100 (Write Q/q to exit):"
                );

                if (string.IsNullOrWhiteSpace(input))
                {
                    _changeBoardSizeDisplay.InformInvalidNumber(input);
                    continue;
                }

                if (_commonInputsOutputsDisplay.UserSelectedExitToMainMenu(input))
                {
                    return NavigationActions.EXIT_TO_MAIN_MENU;
                }

                var isValidNumber = int.TryParse(input, out var newSize) && newSize > 0 && newSize <= 100;
                if (!isValidNumber)
                {
                    _changeBoardSizeDisplay.InformInvalidNumber(input);
                    continue;
                }

                return newSize;
            }
        }
    }
}