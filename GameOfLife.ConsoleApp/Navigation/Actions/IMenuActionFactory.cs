using GameOfLife.Data.Entities;

namespace GameOfLife.ConsoleApp.Navigation.Actions
{
    public interface IMenuActionFactory
    {
        IMenuAction CreateAction(string input, GameFieldDimensions gameFieldDimensions);
    }
}