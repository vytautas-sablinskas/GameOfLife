using GameOfLife.ConsoleApp.Core;
using GameOfLife.Data.Entities;
using System.Text;

namespace GameOfLife.ConsoleApp.ConsoleManagers
{
    public class GameDisplay
    {
        private readonly IConsole _console;

        public GameDisplay(IConsole console)
        {
            _console = console;
        }

        public virtual void SetupConsole(int boardHeight, int boardWidth)
        {
            _console.Clear();
            _console.WriteLine($"The game will display a grid of size {boardHeight}x{boardWidth}.");
            _console.WriteLine("Please adjust the console (zoom out or resize) to ensure the grid will fit.");
            _console.WriteLine("Press Enter when ready...");
            _console.ReadLine();
        }

        public virtual void DisplayField(IGameField gameField)
        {
            _console.Clear();

            InformAboutKeyInputs();
            DisplayIterationAndLiveCellCount(gameField);

            for (int i = 0; i < gameField.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.Cells.GetLength(1); j++)
                {
                    var cellStateSymbol = gameField.Cells[i, j].CurrentState == State.Alive ? "#" : ".";
                    _console.Write(cellStateSymbol);
                }

                _console.WriteLine("");
            }
        }

        public virtual void InformAboutGameStopping()
        {
            _console.Clear();

            _console.WriteLine("You have stopped the game! To continue press Enter.");
            _console.ReadLine();
        }

        public virtual void InformAboutGameSaved(string path)
        {
            _console.Clear();

            _console.WriteLine($"Your progress has been saved to: {path}");
            _console.WriteLine("Press enter to continue...");
            _console.ReadLine();
        }

        public virtual void InformAboutGamePaused()
        {
            _console.WriteLine("Press enter to continue the game!");
            _console.ReadLine();
        }

        private void InformAboutKeyInputs()
        {
            var message = " Keys | 'S' - Stops the game | 'U' - Saves current session | 'P' - Pauses the game ";
            _console.WriteLine(FormatMessage(message));
            _console.WriteLine("");
        }

        private void DisplayIterationAndLiveCellCount(IGameField gameField)
        {
            var message = $" Iteration: '{gameField.IterationCount}' | Live cell count in current iteration: '{gameField.LiveCellCount}' ";
            _console.WriteLine(FormatMessage(message));
        }

        private string FormatMessage(string message)
        {
            var sb = new StringBuilder();

            int width = message.Length + 2;
            sb.Append(new string('-', width) + "\n");
            sb.Append($"|{message}|\n");
            sb.Append(new string('-', width));

            return sb.ToString();
        }
    }
}