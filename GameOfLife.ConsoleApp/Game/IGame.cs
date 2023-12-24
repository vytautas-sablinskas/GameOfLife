namespace GameOfLife.ConsoleApp.Core
{
    public interface IGame
    {
        void Start();

        void SetGameField(IGameField gameField);
    }
}