namespace GameOfLife.ConsoleApp.Files
{
    public interface IGameFileLoader
    {
        void LoadGameFromSavedFile(string savedGame);
    }
}