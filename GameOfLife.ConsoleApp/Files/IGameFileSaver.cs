namespace GameOfLife.ConsoleApp.Files
{
    public interface IGameFileSaver
    {
        string SaveGame(string gameFieldMap);
    }
}