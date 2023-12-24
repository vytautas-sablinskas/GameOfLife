using GameOfLife.Data.Constants;

namespace GameOfLife.ConsoleApp.ConsoleManagers
{
    public class UploadSaveGameDisplay
    {
        private readonly IConsole _console;

        public UploadSaveGameDisplay(IConsole console)
        {
            _console = console;
        }

        public virtual string[] ListSavedGames(string folderPath)
        {
            return Directory.GetFiles(folderPath, "*.txt");
        }

        public void DisplaySavedGames(string[] savedFiles)
        {
            _console.WriteLine("Please choose a saved game (Write Q/q to exit to main menu):");
            for (int i = 0; i < savedFiles.Length; i++)
            {
                _console.WriteLine($"{i + 1}. {Path.GetFileName(savedFiles[i])}");
            }
        }

        public virtual void InformAboutNoFilesFound()
        {
            _console.WriteLine("No saved games found. Press enter to continue");
            _console.ReadLine();
        }

        public virtual string GetSessionFileName()
        {
            string sessionId = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            return Path.Combine(Paths.SAVED_FILES_FOLDER, $"{sessionId}.txt");
        }
    }
}