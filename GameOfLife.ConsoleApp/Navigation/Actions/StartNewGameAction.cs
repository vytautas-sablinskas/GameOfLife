using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.ConsoleApp.Core;
using GameOfLife.Data.Entities;
using GameOfLife.ConsoleApp.Files;
using GameOfLife.ConsoleApp.Rules;

namespace GameOfLife.ConsoleApp.Navigation.Actions
{
    public class StartNewGameAction : IMenuAction
    {
        private readonly IGame _game;

        public StartNewGameAction(IGame game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Start();
        }
    }
}