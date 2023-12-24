using GameOfLife.Data.Constants;

namespace GameOfLife.ConsoleApp.ConsoleManagers
{
    public class ChangeBoardSizeDisplay
    {
        private readonly IConsole _console;

        public ChangeBoardSizeDisplay(IConsole console)
        {
            _console = console;
        }

        public void InformAboutCurrentSelectedInputs(int selectedWidth, int selectedHeight)
        {
            _console.Clear();
            _console.WriteLine("Board size selection (Write Q/q to exit to main menu):");
            _console.WriteLine($"{Words.WIDTH}: {selectedWidth}");
            _console.WriteLine($"{Words.HEIGHT}: {selectedHeight}");
        }

        public virtual void InformInvalidNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                _console.WriteLine("Please provide a valid input.");
                return;
            }

            _console.WriteLine($"\nYou selected an invalid number '{input}', try again.");
            _console.WriteLine("Press any key to continue selecting a new number");
            _console.ReadKey(intercept: true);
        }

        public virtual void InformAboutSuccessfullyChangedValues(int width, int height)
        {
            _console.Clear();
            _console.WriteLine($"Game field dimensions changed to {Words.WIDTH}: {width}, {Words.HEIGHT}: {height}");
            _console.WriteLine("Press any key to return to the main menu.");
            _console.ReadKey(intercept: true);
        }
    }
}