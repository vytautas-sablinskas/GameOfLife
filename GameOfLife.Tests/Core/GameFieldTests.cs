using GameOfLife.ConsoleApp.Core;
using GameOfLife.ConsoleApp.Rules;
using GameOfLife.Data.Entities;
using Moq;

namespace CoreTests
{
    public class GameFieldTests
    {
        private const int DefaultWidth = 3;
        private const int DefaultHeight = 3;
        private const int TotalDefaultCells = DefaultWidth * DefaultHeight;

        private readonly Mock<IGameRules> _gameMockRules;

        public GameFieldTests()
        {
            _gameMockRules = new Mock<IGameRules>();
        }

        [Theory]
        [InlineData(-1, 10)]
        [InlineData(10, -1)]
        public void Constructor_InvalidDimension_ThrowsArgumentException(int width, int height)
        {
            var dimensions = new GameFieldDimensions { Width = width, Height = height };

            Assert.Throws<ArgumentException>(() => new GameField(dimensions, _gameMockRules.Object));
        }

        [Fact]
        public void UpdateField_UpdatesProperties()
        {
            SetupMockRulesForState(State.Alive);
            var gameField = CreateGameFieldWithDefaultDimensions();

            gameField.UpdateField();

            Assert.Equal(TotalDefaultCells, gameField.LiveCellCount);
            Assert.Equal(1, gameField.IterationCount);
        }

        [Fact]
        public void ToString_GeneratesExpectedStringRepresentation()
        {
            string expectedOutput = "1\r\n0\r\n...\r\n...\r\n...\r\n";
            SetupMockRulesForState(State.Dead);
            var gameField = CreateGameFieldWithDefaultDimensions();

            gameField.UpdateField();
            var gameFieldOuput = gameField.ToString();

            Assert.Equal(expectedOutput, gameFieldOuput);
        }

        private void SetupMockRulesForState(State state)
        {
            _gameMockRules.Setup(r => r.DetermineNextState(It.IsAny<Cell>(), It.IsAny<List<Cell>>()))
                          .Returns(state);
        }

        private GameField CreateGameFieldWithDefaultDimensions()
        {
            var dimensions = new GameFieldDimensions { Width = DefaultWidth, Height = DefaultHeight };
            return new GameField(dimensions, _gameMockRules.Object);
        }
    }
}