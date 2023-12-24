using GameOfLife.ConsoleApp.Conditions;
using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.ConsoleApp.Core;

public class GameInputListener : IGameInputListener
{
    public event Action StopGameRequested;

    public event Action InformationSaveRequested;

    public event Action PauseRequested;

    private readonly IConsole _console;
    private readonly IRunCondition _runCondition;

    public GameInputListener(IConsole console, IRunCondition runCondition)
    {
        _console = console;
        _runCondition = runCondition;
    }

    public void StartListening()
    {
        while (_runCondition.ShouldContinue())
        {
            var key = _console.ReadKey(intercept: true).Key;

            if (key == ConsoleKey.S)
            {
                StopGameRequested?.Invoke();
            }
            else if (key == ConsoleKey.U)
            {
                InformationSaveRequested?.Invoke();
            }
            else if (key == ConsoleKey.P)
            {
                PauseRequested?.Invoke();
            }
        }
    }
}