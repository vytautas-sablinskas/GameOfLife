using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.ConsoleApp.Core;
using GameOfLife.ConsoleApp.Rules;
using GameOfLife.Data.Entities;

namespace GameOfLife.ConsoleApp.Files
{
    public class GameFileLoader : IGameFileLoader
    {
        private const int EXTRA_SPOTS_FOR_ITERATION_CELL_COUNTS = 2;
        private readonly CommonInputsOutputsDisplay _commonInputsOutputsDisplay;
        private readonly IGame _game;

        public GameFileLoader(CommonInputsOutputsDisplay commonInputsOutputsDisplay, IGame game)
        {
            _commonInputsOutputsDisplay = commonInputsOutputsDisplay;
            _game = game;
        }

        private GameField ExtractGameFieldFromSavedString(string savedGame)
        {
            try
            {
                var lines = savedGame.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                int iterationCount = int.Parse(lines[0]);

                int height = lines.Length - EXTRA_SPOTS_FOR_ITERATION_CELL_COUNTS;
                int width = lines[2].Length;

                var gameRules = new GameRules(Settings.GAME_RULES);
                var gameFieldDimensions = new GameFieldDimensions(width, height);
                var gameField = new GameField(gameFieldDimensions, gameRules, iterationCount);

                for (int row = 0; row < height; row++)
                {
                    for (int column = 0; column < width; column++)
                    {
                        gameField.Cells[row, column].CurrentState = lines[row + 2][column] == '#' ? State.Alive : State.Dead;
                    }
                }

                return gameField;
            }
            catch
            {
                return null;
            }
        }

        public void LoadGameFromSavedFile(string savedGame)
        {
            var gameField = ExtractGameFieldFromSavedString(savedGame);
            if (gameField == null)
            {
                var message = $"Your selected file is not readable! Fix the file or delete it. Press enter to continue.";
                _commonInputsOutputsDisplay.InformInvalidChoice(message);
                return;
            }

            _game.SetGameField(gameField);

            _game.Start();
        }
    }
}