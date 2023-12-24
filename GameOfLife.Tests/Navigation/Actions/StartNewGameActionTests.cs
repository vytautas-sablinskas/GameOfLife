using GameOfLife.ConsoleApp.Core;
using GameOfLife.ConsoleApp.Navigation.Actions;
using Moq;

namespace NavigationTests
{
    public class StartNewGameActionTests
    {
        private readonly Mock<IGame> _game;
        private readonly StartNewGameAction _startNewGameAction;

        public StartNewGameActionTests()
        {
            _game = new Mock<IGame>();
            _startNewGameAction = new StartNewGameAction(_game.Object);
        }

        [Fact]
        public void Execute_ShouldStartGame()
        {
            _startNewGameAction.Execute();

            _game.Verify(g => g.Start(), Times.Once());
        }
    }
}