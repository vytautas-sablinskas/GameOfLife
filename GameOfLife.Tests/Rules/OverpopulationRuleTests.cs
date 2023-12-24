using GameOfLife.ConsoleApp.Rules;
using GameOfLife.Data.Entities;

namespace RuleTests
{
    public class OverpopulationRuleTests
    {
        private readonly OverpopulationRule _rule;
        private readonly Cell _cell;

        public OverpopulationRuleTests()
        {
            _rule = new OverpopulationRule();
            _cell = new Cell(new Position(1, 1), State.Alive);
        }

        [Fact]
        public void Evaluate_AliveCellWithMoreThanThreeNeighbors_ReturnsDead()
        {
            var result = _rule.Evaluate(_cell, 4);

            Assert.Equal(State.Dead, result);
        }

        [Fact]
        public void Evaluate_AliveCellWithThreeOrLessNeighbors_ReturnsNull()
        {
            var result = _rule.Evaluate(_cell, 3);

            Assert.Null(result);
        }
    }
}