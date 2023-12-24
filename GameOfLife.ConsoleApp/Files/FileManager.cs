using GameOfLife.Data.Constants;

namespace GameOfLife.ConsoleApp.Files
{
    public class FileManager
    {
        public string[] ReadFileToString(string path) => File.ReadAllLines(path);

        public virtual void SaveToFile(string path, string content) => File.WriteAllText(path, content);

        public void CreateSavedFilesFolderIfNotExisting(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}