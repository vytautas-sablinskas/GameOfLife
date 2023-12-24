namespace GameOfLife.ConsoleApp.ConsoleManagers
{
    public interface IConsole
    {
        void Clear();

        void Write(string message);

        void WriteLine(string message);

        string ReadLine();

        ConsoleKeyInfo ReadKey(bool intercept);
    }
}