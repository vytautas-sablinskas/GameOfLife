using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.Data.Constants;
using GameOfLife.ConsoleApp.Files;
using GameOfLife.ConsoleApp.Navigation.Actions;
using GameOfLife.ConsoleApp.Conditions;

namespace GameOfLife.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileManager = new FileManager();
            fileManager.CreateSavedFilesFolderIfNotExisting(Paths.SAVED_FILES_FOLDER);

            NavigationMenu menu = new NavigationMenu(new MenuActionFactory(),
                                                     new NavigationDisplay(),
                                                     new CommonInputsOutputsDisplay(new RealConsole()),
                                                     new AlwaysRunCondition());
            menu.ShowMenu();
        }
    }
}