using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.Data.Constants;
using GameOfLife.Data.Entities;
using GameOfLife.ConsoleApp.Files;
using GameOfLife.ConsoleApp.Services;
using GameOfLife.ConsoleApp.Core;
using GameOfLife.ConsoleApp.Rules;
using GameOfLife.ConsoleApp.Navigation.Services;
using GameOfLife.ConsoleApp.Conditions;

namespace GameOfLife.ConsoleApp.Navigation.Actions
{
    public class MenuActionFactory : IMenuActionFactory
    {
        public IMenuAction CreateAction(string input, GameFieldDimensions gameFieldDimensions)
        {
            var consoleAbstraction = new RealConsole();

            switch (input)
            {
                case NavigationActions.START_NEW_GAME:
                    return new StartNewGameAction(InitializeGame(gameFieldDimensions, consoleAbstraction));

                case NavigationActions.CHANGE_BOARD_SIZE:
                    return new ChangeGameFieldDimensionsAction(gameFieldDimensions,
                                                               new DimensionSelectionService(new CommonInputsOutputsDisplay(consoleAbstraction), new ChangeBoardSizeDisplay(consoleAbstraction)),
                                                               new ChangeBoardSizeDisplay(consoleAbstraction));

                case NavigationActions.UPLOAD_SAVED_FILE:
                    var gameFileLoader = new GameFileLoader(new CommonInputsOutputsDisplay(consoleAbstraction), InitializeGame(gameFieldDimensions, consoleAbstraction));
                    var fileSelectionService = new FileSelectionService(new UploadSaveGameDisplay(consoleAbstraction), new CommonInputsOutputsDisplay(consoleAbstraction));

                    return new UploadSavedGameAction(gameFileLoader, fileSelectionService);

                case NavigationActions.EXIT_APP:
                    return new ExitApplicationAction();

                default:
                    return null;
            }
        }

        private Game InitializeGame(GameFieldDimensions gameFieldDimensions, IConsole consoleAbstraction)
        {
            var gameRules = new GameRules(Settings.GAME_RULES);
            var gameField = new GameField(gameFieldDimensions, gameRules);
            var gameInputListener = new GameInputListener(new RealConsole(), new AlwaysRunCondition());
            var gameDisplay = new GameDisplay(new RealConsole());
            var gameSaver = new GameFileSaver(new FileManager(), new UploadSaveGameDisplay(consoleAbstraction));

            return new Game(gameField, gameInputListener, gameDisplay, gameSaver);
        }
    }
}