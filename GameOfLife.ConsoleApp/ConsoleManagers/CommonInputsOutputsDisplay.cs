namespace GameOfLife.ConsoleApp.ConsoleManagers
{
    public class CommonInputsOutputsDisplay
    {
        private readonly IConsole _console;

        public CommonInputsOutputsDisplay(IConsole console)
        {
            _console = console;
        }

        public virtual string PromptInput(string messageToUser)
        {
            _console.WriteLine(messageToUser);
            return _console.ReadLine();
        }

        public bool UserSelectedExitToMainMenu(string userInput) =>
            userInput.Equals("Q", StringComparison.OrdinalIgnoreCase);

        public virtual void InformInvalidChoice(string input = "")
        {
            var message = string.IsNullOrEmpty(input) ? "Invalid choice. Press enter to try again." : input;

            _console.Clear();
            _console.WriteLine(message);
            _console.ReadLine();
        }

        public void ClearMessages()
        {
            _console.Clear();
        }
    }
}