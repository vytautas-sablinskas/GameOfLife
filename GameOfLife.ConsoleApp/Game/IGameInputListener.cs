namespace GameOfLife.ConsoleApp.Core
{
    public interface IGameInputListener
    {
        event Action StopGameRequested;

        event Action InformationSaveRequested;

        event Action PauseRequested;

        void StartListening();
    }
}