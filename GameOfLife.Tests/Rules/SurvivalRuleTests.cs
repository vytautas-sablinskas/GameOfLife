using GameOfLife.ConsoleApp.Rules;
using GameOfLife.Data.Entities;

namespace RuleTests
{
    public class SurvivalRuleTests
    {
        private readonly SurvivalRule _rule;
        private readonly Cell _cell;

        public SurvivalRuleTests()
        {
            _rule = new SurvivalRule();
            _cell = new Cell(new Position(1, 1), State.Alive);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Evaluate_AliveCellWithTwoOrThreeNeighbors_ReturnsAlive(int liveNeighbors)
        {
            var result = _rule.Evaluate(_cell, liveNeighbors);
            Assert.Equal(State.Alive, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void Evaluate_AliveCellWithNonTwoOrThreeNeighbors_ReturnsNull(int liveNeighbors)
        {
            var result = _rule.Evaluate(_cell, liveNeighbors);
            Assert.Null(result);
        }
    }
}